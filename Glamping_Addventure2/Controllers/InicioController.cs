using Microsoft.AspNetCore.Mvc;
using Glamping_Addventure.Models.Recursos;
using Glamping_Addventure.Models.Servicios.Contrato;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Glamping_Addventure2.Models;

namespace Glamping_Addventure.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;

        public InicioController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpGet]

        public IActionResult Registrarse()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario modelo, int Idrol, string confirmarContrasena)
        {
            // Validar selección de rol
            if (Idrol <= 0)
            {
                ModelState.AddModelError("Idrol", "Debe seleccionar un tipo de rol.");
            }

            // Validar coincidencia de contraseñas
            if (string.IsNullOrWhiteSpace(confirmarContrasena) || modelo.Contrasena != confirmarContrasena)
            {
                ModelState.AddModelError("Contrasena", "Las contraseñas no coinciden.");
            }

            // Validar existencia de email y documento
            var usuarioExistente = await _usuarioServicio.GetUsuarioPorEmail(modelo.Email);
            if (usuarioExistente != null)
            {
                ModelState.AddModelError("Email", "El correo electrónico ya está registrado.");
            }

            var documentoExistente = await _usuarioServicio.GetUsuarioPorDocumento(modelo.NumeroDocumento);
            if (documentoExistente != null)
            {
                ModelState.AddModelError("NumeroDocumento", "El número de documento ya está registrado.");
            }

            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            modelo.Contrasena = Utilidades.EncriptarClave(modelo.Contrasena);
            modelo.Idrol = Idrol;

            Usuario usuario_creado = await _usuarioServicio.SaveUsuario(modelo);
            if (usuario_creado != null && usuario_creado.Idusuario > 0)
            {
                return RedirectToAction("IniciarSesion", "Inicio");
            }

            ModelState.AddModelError("", "No se pudo crear el usuario.");
            return View(modelo);
        }


        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string email, string contrasena)
        {
            Usuario usuario_encontrado = await _usuarioServicio.GetUsuario(email, Utilidades.EncriptarClave(contrasena));

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            // Verifica si IdrolNavigation es nulo
            string rolNombre = usuario_encontrado.IdrolNavigation?.Nombre ?? "Usuario"; // Asigna un valor por defecto si es null

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario_encontrado.NombreUsuario),
                new Claim(ClaimTypes.Role, rolNombre) // Usa el nombre del rol o un valor por defecto
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            // Guarda la última actividad del usuario en la sesión
            HttpContext.Session.SetString("LastActivity", DateTime.UtcNow.ToString());

            return RedirectToAction("Index", "Home");
        }

        public bool VerifyPassword(string plainTextPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
        }


    }


}
