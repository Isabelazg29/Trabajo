using Microsoft.AspNetCore.Mvc;
using Glamping_Addventure2.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization; 

namespace Glamping_Addventure2.Controllers
{
    [Authorize(Roles = "9")]
    public class RolesController : Controller
    {
        private readonly GlampingAddventureContext _context;

        public RolesController(GlampingAddventureContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var roles = _context.Roles.Include(r => r.RolesPermisos).ToList(); // Incluyendo permisos para mostrar más información si es necesario
            return View(roles);
        }

        [HttpPost]
public IActionResult ToggleActive(int id, bool isActive)
{
    var role = _context.Roles.FirstOrDefault(r => r.Idrol == id);
    if (role == null)
    {
        return Json(new { success = false, message = "Rol no encontrado" });
    }

    // Cambiar estado y guardar cambios
    role.IsActive = isActive;
    _context.SaveChanges();

    return Json(new { success = true, message = "Estado del rol actualizado correctamente" });
}


        public IActionResult Create()
        {
            ViewBag.Permisos = _context.Permisos.ToList(); // Cargar permisos desde la tabla Permisos
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Role role, int[] selectedPermisos, bool isActive)
        {
            if (ModelState.IsValid)
            {
                role.IsActive = isActive; // Establecer el estado del rol según el checkbox
                _context.Roles.Add(role);
                _context.SaveChanges();

                // Asociar permisos seleccionados
                if (selectedPermisos != null)
                {
                    foreach (var permisoId in selectedPermisos)
                    {
                        var rolPermiso = new RolesPermiso
                        {
                            Idrol = role.Idrol,
                            Idpermiso = permisoId
                        };
                        _context.RolesPermisos.Add(rolPermiso);
                    }
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Permisos = _context.Permisos.ToList(); // Re-cargar permisos en caso de error
            return View(role);
        }
        // GET: Roles/Edit/5
        public IActionResult Edit(int id)
        {
            var role = _context.Roles.Include(r => r.RolesPermisos).FirstOrDefault(r => r.Idrol == id);
            if (role == null)
            {
                return NotFound();
            }

            // Cargar todos los permisos disponibles
            var allPermisos = _context.Permisos.ToList();

            // Pasar el rol y los permisos a la vista
            ViewBag.Permisos = allPermisos;
            ViewBag.PermisosAsignados = role.RolesPermisos.Select(p => p.Idpermiso).ToList();

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Role role, int[] selectedPermisos, bool isActive)
        {
            if (id != role.Idrol)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar el estado del rol
                    role.IsActive = isActive;
                    _context.Update(role);
                    _context.SaveChanges();

                    // Actualizar los permisos asociados
                    // Eliminar los permisos actuales
                    var existingPermissions = _context.RolesPermisos.Where(rp => rp.Idrol == id).ToList();
                    _context.RolesPermisos.RemoveRange(existingPermissions);
                    _context.SaveChanges();

                    // Agregar los nuevos permisos seleccionados
                    if (selectedPermisos != null)
                    {
                        foreach (var permisoId in selectedPermisos)
                        {
                            var rolPermiso = new RolesPermiso
                            {
                                Idrol = role.Idrol,
                                Idpermiso = permisoId
                            };
                            _context.RolesPermisos.Add(rolPermiso);
                        }
                        _context.SaveChanges();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Roles.Any(e => e.Idrol == role.Idrol))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Permisos = _context.Permisos.ToList();
            ViewBag.PermisosAsignados = selectedPermisos;
            return View(role);
        }

        // GET: Roles/Delete/5
        public IActionResult Delete(int id)
        {
            var rol = _context.Roles.Include(r => r.RolesPermisos).FirstOrDefault(r => r.Idrol == id);
            if (rol == null)
            {
                return NotFound();
            }

            // Verificar si el rol está asociado a usuarios
            var usuariosConRol = _context.Usuarios.Where(u => u.Idrol == id).ToList();
            ViewBag.HasUsers = usuariosConRol.Any(); // Pasar información a la vista

            return View(rol);
        }


        // POST: Roles/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var rol = _context.Roles.Include(r => r.RolesPermisos).FirstOrDefault(r => r.Idrol == id);
            if (rol == null)
            {
                return NotFound();
            }

            // Obtener los usuarios que tienen este rol
            var usuariosConRol = _context.Usuarios.Where(u => u.Idrol == id).ToList();

            if (usuariosConRol.Any())
            {
                // Opción 1: Eliminar los usuarios asociados
                _context.Usuarios.RemoveRange(usuariosConRol);
                // Opción 2: Cambiar el rol de los usuarios (por ejemplo, asignar un rol por defecto)
                // foreach (var usuario in usuariosConRol)
                // {
                //     usuario.IDRol = null; // o asignar otro rol
                // }

                // Guardar cambios en la base de datos
                _context.SaveChanges();
            }

            // Eliminar los permisos relacionados
            _context.RolesPermisos.RemoveRange(rol.RolesPermisos);
            // Eliminar el rol
            _context.Roles.Remove(rol);

            // Guardar cambios en la base de datos
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


    }
}