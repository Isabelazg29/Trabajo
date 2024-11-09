using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Glamping_Addventure2.Models;
using Glamping_Addventure2.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Glamping_Addventure.Models.Servicios.Contrato;
using Glamping_Addventure.Models.Servicios.Implementación;
using Microsoft.AspNetCore.Authentication; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configura el contexto de la base de datos
builder.Services.AddDbContext<GlampingAddventureContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BloggingDatabase")));

// Registra servicios personalizados
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEmailService, EmailService>(); // Asegúrate de registrar el servicio de correo

// Configuración de autenticación
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Inicio/IniciarSesion";
        options.AccessDeniedPath = "/Home/Index"; // Redirigir a la acción AccessDenied en RolesController
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true; // Permite renovar el tiempo de sesión al interactuar
    });

// Configuración de autorización para roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administradores", policy => policy.RequireRole("Administrador"));
    options.AddPolicy("Recepcionistas", policy => policy.RequireRole("Recepcionista"));
});

// Configuración de la sesión
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Cambia esto según tus necesidades
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Necesario para que la sesión funcione
});

// Configuración de filtros para controlar el almacenamiento en caché
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new ResponseCacheAttribute
    {
        NoStore = true,
        Location = ResponseCacheLocation.None,
    });
});

var app = builder.Build();

// Añadir el middleware para gestionar el tiempo de sesión
app.UseSession(); // Asegúrate de usar la sesión antes de usar el middleware
app.UseMiddleware<SessionTimeoutMiddleware>(); // Añadir el middleware de sesión

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Puedes añadir HSTS para mejorar el seguridad
}

app.UseHttpsRedirection(); // Asegúrate de redirigir a HTTPS
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Configura las rutas de los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}");

// Ruta para el controlador de recuperación (sin prefijo api)
app.MapControllerRoute(
    name: "recuperacion",
    pattern: "Recuperacion/{action=SolicitarRecuperacion}/{id?}",
    defaults: new { controller = "Recuperacion", action = "SolicitarRecuperacion" });

app.Run();

// Middleware para gestionar el tiempo de sesión
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
                if (DateTime.UtcNow - lastActivity > TimeSpan.FromMinutes(20)) // Cambia el tiempo aquí según tu necesidad
                {
                    await Logout(context);
                }
            }

            // Actualiza la última actividad
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
