using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Glamping_Addventure2.Models;
using Glamping_Addventure2.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Glamping_Addventure.Models.Servicios.Contrato;
using Glamping_Addventure.Models.Servicios.Implementaci�n;
using Microsoft.AspNetCore.Authentication; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configura el contexto de la base de datos
builder.Services.AddDbContext<GlampingAddventureContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BloggingDatabase")));

// Registra servicios personalizados
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEmailService, EmailService>(); // Aseg�rate de registrar el servicio de correo

// Configuraci�n de autenticaci�n
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Inicio/IniciarSesion";
        options.AccessDeniedPath = "/Home/Index"; // Redirigir a la acci�n AccessDenied en RolesController
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true; // Permite renovar el tiempo de sesi�n al interactuar
    });

// Configuraci�n de autorizaci�n para roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administradores", policy => policy.RequireRole("Administrador"));
    options.AddPolicy("Recepcionistas", policy => policy.RequireRole("Recepcionista"));
});

// Configuraci�n de la sesi�n
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Cambia esto seg�n tus necesidades
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Necesario para que la sesi�n funcione
});

// Configuraci�n de filtros para controlar el almacenamiento en cach�
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new ResponseCacheAttribute
    {
        NoStore = true,
        Location = ResponseCacheLocation.None,
    });
});

var app = builder.Build();

// A�adir el middleware para gestionar el tiempo de sesi�n
app.UseSession(); // Aseg�rate de usar la sesi�n antes de usar el middleware
app.UseMiddleware<SessionTimeoutMiddleware>(); // A�adir el middleware de sesi�n

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Puedes a�adir HSTS para mejorar el seguridad
}

app.UseHttpsRedirection(); // Aseg�rate de redirigir a HTTPS
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Configura las rutas de los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}");

// Ruta para el controlador de recuperaci�n (sin prefijo api)
app.MapControllerRoute(
    name: "recuperacion",
    pattern: "Recuperacion/{action=SolicitarRecuperacion}/{id?}",
    defaults: new { controller = "Recuperacion", action = "SolicitarRecuperacion" });

app.Run();

// Middleware para gestionar el tiempo de sesi�n
public class SessionTimeoutMiddleware
{
    private readonly RequestDelegate _next;

    public SessionTimeoutMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var lastActivityString = context.Session.GetString("LastActivity");
            if (lastActivityString != null)
            {
                var lastActivity = DateTime.Parse(lastActivityString);
                if (DateTime.UtcNow - lastActivity > TimeSpan.FromMinutes(20)) // Cambia el tiempo aqu� seg�n tu necesidad
                {
                    await Logout(context);
                }
            }

            // Actualiza la �ltima actividad
            context.Session.SetString("LastActivity", DateTime.UtcNow.ToString());
        }

        await _next(context);
    }

    private async Task Logout(HttpContext context)
    {
        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        context.Response.Redirect("/Inicio/IniciarSesion");
    }
}
