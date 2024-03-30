using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlexaSoft_ASP.NET.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlexaSoft_ASP.NET.Controllers
{
    public class DetallesProductosXCompraController : Controller
    {
        private readonly AlexasoftContext _context;

        public DetallesProductosXCompraController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: DetallesProductosXCompra
        public async Task<IActionResult> Index()
        {
            return _context.Detallesproductosxcompras != null ?
                          View(await _context.Detallesproductosxcompras.ToListAsync()) :
                          Problem("Entity set 'AlexasoftContext.Detallesproductosxcompras'  is null.");
        }

        // GET: DetallesProductosXCompra/Details/5
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

        // GET: DetallesProductosXCompra/Create
        public IActionResult Create()
        {
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre");
            return View();
        }

        // POST: DetallesProductosXCompra/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalleProductoXcompra,IdProducto,IdCompra,Unidades")] Detallesproductosxcompra detallesproductosxcompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallesproductosxcompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", detallesproductosxcompra.IdCompra);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", detallesproductosxcompra.IdProducto);
            return View(detallesproductosxcompra);
        }

        // GET: DetallesProductosXCompra/Edit/5
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", detallesproductosxcompra.IdProducto);
            return View(detallesproductosxcompra);
        }

        // POST: DetallesProductosXCompra/Edit/5
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", detallesproductosxcompra.IdProducto);
            return View(detallesproductosxcompra);
        }

        // GET: DetallesProductosXCompra/Delete/5
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

        // POST: DetallesProductosXCompra/Delete/5
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
