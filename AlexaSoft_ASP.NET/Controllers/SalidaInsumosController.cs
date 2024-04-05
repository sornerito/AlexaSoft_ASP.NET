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
    public class SalidaInsumosController : Controller
    {
        private readonly AlexasoftContext _context;

        public SalidaInsumosController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: SalidaInsumos
        public async Task<IActionResult> Index()
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Insumos"))
            {
                return RedirectToAction("Error", "Home");
            }
            var alexasoftContext = _context.SalidaInsumos.Include(s => s.IdProductoNavigation);
            return View(await alexasoftContext.ToListAsync());
        }

        // GET: SalidaInsumos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Insumos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.SalidaInsumos == null)
            {
                return NotFound();
            }

            var salidaInsumo = await _context.SalidaInsumos
                .Include(s => s.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdInsumo == id);
            if (salidaInsumo == null)
            {
                return NotFound();
            }

            return View(salidaInsumo);
        }

        // GET: SalidaInsumos/Create
        public IActionResult Create()
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Insumos"))
            {
                return RedirectToAction("Error", "Home");
            }
            var productosActivos = _context.Productos.Where(p => p.Estado == "Activo").ToList();

           
            var productosSelectList = productosActivos.Select(p => new SelectListItem
            {
                Value = p.IdProducto.ToString(),
                Text = p.Nombre
            }).ToList();

            
            ViewData["IdProducto"] = new SelectList(productosSelectList, "Value", "Text");


            return View();
        }

        // POST: SalidaInsumos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInsumo,IdProducto,FechaRetiro,Cantidad,MotivoAnular")] SalidaInsumo salidaInsumo)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Insumos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (ModelState.IsValid)
            {
                
                var producto = await _context.Productos.FindAsync(salidaInsumo.IdProducto);

                if (producto != null)
                {
                    
                    if (salidaInsumo.Cantidad > producto.Unidades)
                    {
                        ModelState.AddModelError("Cantidad", "La cantidad deseada excede la cantidad disponible.");
                        ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", salidaInsumo.IdProducto);
                        return View(salidaInsumo);
                    }

                    producto.Unidades -= salidaInsumo.Cantidad;
                    _context.Update(producto);
                }

               
                _context.Add(salidaInsumo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", salidaInsumo.IdProducto);
            return View(salidaInsumo);
        }



        // GET: SalidaInsumos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Insumos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.SalidaInsumos == null)
            {
                return NotFound();
            }

            var salidaInsumo = await _context.SalidaInsumos.FindAsync(id);
            if (salidaInsumo == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", salidaInsumo.IdProducto);
            return View(salidaInsumo);
        }

        // POST: SalidaInsumos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInsumo,IdProducto,FechaRetiro,Cantidad,MotivoAnular")] SalidaInsumo salidaInsumo)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Insumos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id != salidaInsumo.IdInsumo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                { 
                    if (!string.IsNullOrEmpty(salidaInsumo.MotivoAnular))
                    {
                        // Obtener el producto correspondiente
                        var producto = await _context.Productos.FindAsync(salidaInsumo.IdProducto);
                        if (producto != null)
                        {
                            producto.Unidades += salidaInsumo.Cantidad;
                            _context.Update(producto);
                        }
                    }
                    _context.Update(salidaInsumo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalidaInsumoExists(salidaInsumo.IdInsumo))
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", salidaInsumo.IdProducto);
            return View(salidaInsumo);
        }

        // GET: SalidaInsumos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Insumos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.SalidaInsumos == null)
            {
                return NotFound();
            }

            var salidaInsumo = await _context.SalidaInsumos
                .Include(s => s.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdInsumo == id);
            if (salidaInsumo == null)
            {
                return NotFound();
            }

            return View(salidaInsumo);
        }

        // POST: SalidaInsumos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Insumos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (_context.SalidaInsumos == null)
            {
                return Problem("Entity set 'AlexasoftContext.SalidaInsumos'  is null.");
            }
            var salidaInsumo = await _context.SalidaInsumos.FindAsync(id);
            if (salidaInsumo != null)
            {
                _context.SalidaInsumos.Remove(salidaInsumo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalidaInsumoExists(int id)
        {
          return (_context.SalidaInsumos?.Any(e => e.IdInsumo == id)).GetValueOrDefault();
        }
    }
}
