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
    public class PaquetesController : Controller
    {
        private readonly AlexasoftContext _context;

        public PaquetesController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: Paquetes
        public async Task<IActionResult> Index()
        {
            
            var paquetesconServicios = _context.Paquetes
            .Include(p => p.PaquetesServicios)
                .ThenInclude(Ps => Ps.IdServicioNavigation)
            .ToList();

            return View(paquetesconServicios);
        }

        // GET: Paquetes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Paquetes == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquetes
                .FirstOrDefaultAsync(m => m.IdPaquete == id);
            if (paquete == null)
            {
                return NotFound();
            }

            return View(paquete);
        }

        // GET: Paquetes/Create 
        public IActionResult Create()
        {
            var serviciosActivos = _context.Servicios.Where(s => s.Estado == "Activo").ToList();
            ViewBag.servicios = serviciosActivos;


            return View();
        }

        // POST: Paquetes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Paquete role, double[] servicios)
        {         
                _context.Paquetes.Add(role);
                _context.SaveChanges();

                foreach (int permisoId in servicios)
                {

                    PaquetesServicio rolesPermiso = new PaquetesServicio
                    {
                        IdPaquete = role.IdPaquete,
                        IdServicio = permisoId
                    };

                    _context.PaquetesServicios.Add(rolesPermiso);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            
            return View(role);
        }

        // GET: Paquetes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var rol = await _context.Paquetes
            .Include(r => r.PaquetesServicios)
            .ThenInclude(rp => rp.IdServicioNavigation)
            .FirstOrDefaultAsync(r => r.IdPaquete == id);
            if (rol == null)
            {
                return NotFound();
            }
            var servicio = _context.Servicios.ToList();
            ViewBag.Servicios = servicio;
            return View(rol);
        }

        // POST: Roles/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Paquete role, int[] servicios)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    //Guarda datos de la tabla Rol (solo el nombre practicamente)
                    _context.Update(role);
                    await _context.SaveChangesAsync();//actualiza cambios

                    //el rol con los registros que le corresponden de la tabla RolesPermisos
                    var rol = await _context.Paquetes
                        .Include(r => r.PaquetesServicios)
                        .FirstOrDefaultAsync(r => r.IdPaquete == id);

                    //le hace un ciclo a cada registro, y revisa que este dentro de los permisos enviados en el formulario
                    foreach (var rp in rol.PaquetesServicios.ToList())
                    {
                        if (!servicios.Contains(rp.IdServicio))
                        {
                            //si en la lista de checkboxes ya no se encuentra el id, significa que hay que removerlo
                            _context.PaquetesServicios.Remove(rp);
                        }
                    }

                    //esta lista reccorre los checkboxes (idPermisos) enviados por el formulario
                    foreach (var servicioId in servicios)
                    {
                        //Si un permiso ENVIADO no coincide con ninguno de los permisos que ya estaban, SE CREA
                        if (!rol.PaquetesServicios.Any(rp => rp.IdServicio == servicioId))
                        {
                            PaquetesServicio rolesPermiso = new PaquetesServicio
                            {
                                IdPaquete = role.IdPaquete,
                                IdServicio = servicioId
                            };
                            _context.PaquetesServicios.Add(rolesPermiso);
                        }
                        //Si se encuentra el Id ENVIADO en los Ids que YA ESTABAN, no se hace nada
                    }
                    //Se guardan los cambios una sola vez
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Paquetes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Paquetes == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquetes
                .FirstOrDefaultAsync(m => m.IdPaquete == id);
            if (paquete == null)
            {
                return NotFound();
            }

            return View(paquete);
        }

        // POST: Paquetes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Paquetes == null)
            {
                return Problem("Entity set 'AlexasoftContext.Paquetes'  is null.");
            }
            var paquete = await _context.Paquetes.FindAsync(id);
            if (paquete != null)
            {
                _context.Paquetes.Remove(paquete);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaqueteExists(int id)
        {
          return (_context.Paquetes?.Any(e => e.IdPaquete == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarEstado(int idpaquete)
        {
           
            var rol = await _context.Paquetes.FindAsync(idpaquete);
            if (rol == null)
            {
                return NotFound();
            }

            rol.Estado = rol.Estado == "Activo" ? "Desactivado" : "Activo";
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
