using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlexaSoft_ASP.NET.Models;

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
            var alexasoftContext = _context.SalidaInsumos.Include(s => s.IdProductoNavigation);
            return View(await alexasoftContext.ToListAsync());
        }

        // GET: SalidaInsumos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre");
            return View();
        }

        // POST: SalidaInsumos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInsumo,IdProducto,FechaRetiro,Cantidad,MotivoAnular")] SalidaInsumo salidaInsumo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salidaInsumo);

                // Actualizar las unidades del producto
                var producto = await _context.Productos.FindAsync(salidaInsumo.IdProducto);
                if (producto != null)
                {
                    producto.Unidades -= salidaInsumo.Cantidad;
                    _context.Update(producto);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", salidaInsumo.IdProducto);
            return View(salidaInsumo);
        }


        // GET: SalidaInsumos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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
            if (id != salidaInsumo.IdInsumo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
