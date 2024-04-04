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
    public class VentasdetalleproductosController : Controller
    {
        private readonly AlexasoftContext _context;

        public VentasdetalleproductosController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: Ventasdetalleproductos
        public async Task<IActionResult> Index()
        {
            var alexasoftContext = _context.Ventasdetalleproductos.Include(v => v.IdProductoNavigation).Include(v => v.IdVentaNavigation);
            return View(await alexasoftContext.ToListAsync());
        }

        // GET: Ventasdetalleproductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ventasdetalleproductos == null)
            {
                return NotFound();
            }

            var ventasdetalleproducto = await _context.Ventasdetalleproductos
                .Include(v => v.IdProductoNavigation)
                .Include(v => v.IdVentaNavigation)
                .FirstOrDefaultAsync(m => m.IdVentaDetalleProducto == id);
            if (ventasdetalleproducto == null)
            {
                return NotFound();
            }

            return View(ventasdetalleproducto);
        }

        // GET: Ventasdetalleproductos/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta");
            return View();
        }

        // POST: Ventasdetalleproductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVentaDetalleProducto,IdVenta,IdProducto,PrecioUnitario,Cantidad")] Ventasdetalleproducto ventasdetalleproducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventasdetalleproducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", ventasdetalleproducto.IdProducto);
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", ventasdetalleproducto.IdVenta);
            return View(ventasdetalleproducto);
        }

        // GET: Ventasdetalleproductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ventasdetalleproductos == null)
            {
                return NotFound();
            }

            var ventasdetalleproducto = await _context.Ventasdetalleproductos.FindAsync(id);
            if (ventasdetalleproducto == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", ventasdetalleproducto.IdProducto);
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", ventasdetalleproducto.IdVenta);
            return View(ventasdetalleproducto);
        }

        // POST: Ventasdetalleproductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVentaDetalleProducto,IdVenta,IdProducto,PrecioUnitario,Cantidad")] Ventasdetalleproducto ventasdetalleproducto)
        {
            if (id != ventasdetalleproducto.IdVentaDetalleProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventasdetalleproducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentasdetalleproductoExists(ventasdetalleproducto.IdVentaDetalleProducto))
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", ventasdetalleproducto.IdProducto);
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", ventasdetalleproducto.IdVenta);
            return View(ventasdetalleproducto);
        }

        // GET: Ventasdetalleproductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ventasdetalleproductos == null)
            {
                return NotFound();
            }

            var ventasdetalleproducto = await _context.Ventasdetalleproductos
                .Include(v => v.IdProductoNavigation)
                .Include(v => v.IdVentaNavigation)
                .FirstOrDefaultAsync(m => m.IdVentaDetalleProducto == id);
            if (ventasdetalleproducto == null)
            {
                return NotFound();
            }

            return View(ventasdetalleproducto);
        }

        // POST: Ventasdetalleproductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ventasdetalleproductos == null)
            {
                return Problem("Entity set 'AlexasoftContext.Ventasdetalleproductos'  is null.");
            }
            var ventasdetalleproducto = await _context.Ventasdetalleproductos.FindAsync(id);
            if (ventasdetalleproducto != null)
            {
                _context.Ventasdetalleproductos.Remove(ventasdetalleproducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentasdetalleproductoExists(int id)
        {
          return (_context.Ventasdetalleproductos?.Any(e => e.IdVentaDetalleProducto == id)).GetValueOrDefault();
        }
    }
}
