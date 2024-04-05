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
    public class DetallesproductosxcomprasController : Controller
    {
        private readonly AlexasoftContext _context;

        public DetallesproductosxcomprasController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: Detallesproductosxcompras
        public async Task<IActionResult> Index()
        {
            var alexasoftContext = _context.Detallesproductosxcompras.Include(d => d.IdCompraNavigation).Include(d => d.IdProductoNavigation);
            return View(await alexasoftContext.ToListAsync());
        }

        // GET: Detallesproductosxcompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Detallesproductosxcompras == null)
            {
                return NotFound();
            }

            var detallesproductosxcompra = await _context.Detallesproductosxcompras
                .Include(d => d.IdCompraNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleProductoXcompra == id);
            if (detallesproductosxcompra == null)
            {
                return NotFound();
            }

            return View(detallesproductosxcompra);
        }

        // GET: Detallesproductosxcompras/Create
        public IActionResult Create()
        {
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "Fecha");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre");
            return View();
        }

        // POST: Detallesproductosxcompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalleProductoXcompra,IdProducto,IdCompra,Unidades")] Detallesproductosxcompra detallesproductosxcompra)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(detallesproductosxcompra);
                await _context.SaveChangesAsync();

                // Actualizar las unidades del producto correspondiente
                var producto = await _context.Productos.FindAsync(detallesproductosxcompra.IdProducto);
                if (producto != null)
                {
                    producto.Unidades += detallesproductosxcompra.Unidades;
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", detallesproductosxcompra.IdCompra);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detallesproductosxcompra.IdProducto);
            return View(detallesproductosxcompra);
        }

        // GET: Detallesproductosxcompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Detallesproductosxcompras == null)
            {
                return NotFound();
            }

            var detallesproductosxcompra = await _context.Detallesproductosxcompras.FindAsync(id);
            if (detallesproductosxcompra == null)
            {
                return NotFound();
            }
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", detallesproductosxcompra.IdCompra);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detallesproductosxcompra.IdProducto);
            return View(detallesproductosxcompra);
        }

        // POST: Detallesproductosxcompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalleProductoXcompra,IdProducto,IdCompra,Unidades")] Detallesproductosxcompra detallesproductosxcompra)
        {
            if (id != detallesproductosxcompra.IdDetalleProductoXcompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallesproductosxcompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallesproductosxcompraExists(detallesproductosxcompra.IdDetalleProductoXcompra))
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
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", detallesproductosxcompra.IdCompra);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detallesproductosxcompra.IdProducto);
            return View(detallesproductosxcompra);
        }

        // GET: Detallesproductosxcompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Detallesproductosxcompras == null)
            {
                return NotFound();
            }

            var detallesproductosxcompra = await _context.Detallesproductosxcompras
                .Include(d => d.IdCompraNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleProductoXcompra == id);
            if (detallesproductosxcompra == null)
            {
                return NotFound();
            }

            return View(detallesproductosxcompra);
        }

        // POST: Detallesproductosxcompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Detallesproductosxcompras == null)
            {
                return Problem("Entity set 'AlexasoftContext.Detallesproductosxcompras'  is null.");
            }
            var detallesproductosxcompra = await _context.Detallesproductosxcompras.FindAsync(id);
            if (detallesproductosxcompra != null)
            {
                _context.Detallesproductosxcompras.Remove(detallesproductosxcompra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallesproductosxcompraExists(int id)
        {
          return (_context.Detallesproductosxcompras?.Any(e => e.IdDetalleProductoXcompra == id)).GetValueOrDefault();
        }
    }
}
