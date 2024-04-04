using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlexaSoft_ASP.NET.Models;
using AlexaSoft_ASP.NET.Utilities;

namespace AlexaSoft_ASP.NET.Controllers
{
    public class RolesController : Controller
    {
        private readonly AlexasoftContext _context;

        public RolesController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Roles"))
            {
                return RedirectToAction("Error", "Home");
            }
            var rolesConPermisos = _context.Roles
            .Include(r => r.RolesPermisos)
                .ThenInclude(rp => rp.IdPermisoNavigation) 
            .ToList();

            return View(rolesConPermisos);
        }

        

        // GET: Roles/Create
        public IActionResult Create()
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Roles"))
            {
                return RedirectToAction("Error", "Home");
            }
            var permisos = _context.Permisos.ToList();
            ViewBag.Permisos = permisos;
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role role, double[] permisos)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Roles"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Roles.Add(role);
                _context.SaveChanges();

                foreach (int permisoId in permisos)
                {

                    RolesPermiso rolesPermiso = new RolesPermiso
                    {
                        IdRol = role.IdRol,              
                        IdPermiso = permisoId
                    };

                    _context.RolesPermisos.Add(rolesPermiso);
                    _context.SaveChanges();
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Roles"))
            {
                return RedirectToAction("Error", "Home");
            }
            var rol = await _context.Roles
            .Include(r => r.RolesPermisos)
            .ThenInclude(rp => rp.IdPermisoNavigation) 
            .FirstOrDefaultAsync(r => r.IdRol == id);
            if (rol == null)
            {
                return NotFound();
            }
            var permisos = _context.Permisos.ToList();
            ViewBag.Permisos = permisos;
            return View(rol);
        }

        // POST: Roles/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Role role, int[] permisos)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Roles"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    //Guarda datos de la tabla Rol (solo el nombre practicamente)
                    _context.Update(role);
                    await _context.SaveChangesAsync();//actualiza cambios

                    //el rol con los registros que le corresponden de la tabla RolesPermisos
                    var rol = await _context.Roles
                        .Include(r => r.RolesPermisos)
                        .FirstOrDefaultAsync(r => r.IdRol == id);
                    
                    //le hace un ciclo a cada registro, y revisa que este dentro de los permisos enviados en el formulario
                    foreach (var rp in rol.RolesPermisos.ToList())
                    {
                        if (!permisos.Contains(rp.IdPermiso))
                        {
                            //si en la lista de checkboxes ya no se encuentra el id, significa que hay que removerlo
                            _context.RolesPermisos.Remove(rp);
                        }
                    }

                    //esta lista reccorre los checkboxes (idPermisos) enviados por el formulario
                    foreach (var permisoId in permisos)
                    {
                        //Si un permiso ENVIADO no coincide con ninguno de los permisos que ya estaban, SE CREA
                        if (!rol.RolesPermisos.Any(rp => rp.IdPermiso == permisoId))
                        {
                            RolesPermiso rolesPermiso = new RolesPermiso
                            {
                                IdRol = role.IdRol,
                                IdPermiso = permisoId
                            };
                            _context.RolesPermisos.Add(rolesPermiso);
                        }
                        //Si se encuentra el Id ENVIADO en los Ids que YA ESTABAN, no se hace nada
                    }
                    //Se guardan los cambios una sola vez
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.IdRol))
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
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> CambiarEstado(int idRol)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Roles"))
            {
                return RedirectToAction("Error", "Home");
            }
            var rol = await _context.Roles.FindAsync(idRol);
            if (rol == null)
            {
                return NotFound();
            }

            rol.Estado = rol.Estado == "Activo" ? "Desactivado" : "Activo";
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool RoleExists(int id)
        {
          return (_context.Roles?.Any(e => e.IdRol == id)).GetValueOrDefault();
        }
    }
}
