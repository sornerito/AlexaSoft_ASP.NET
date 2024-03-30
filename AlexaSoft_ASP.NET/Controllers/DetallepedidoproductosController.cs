using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlexaSoft_ASP.NET.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlexaSoft_ASP.NET.Controllers
{
    public class DetallesPedidosProductosController : Controller
    {
        private readonly AlexasoftContext _context;

        public DetallesPedidosProductosController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: DetallesPedidosProductos
        public async Task<IActionResult> Index()
        {
            return _context.Detallespedidosproductos != null ?
                          View(await _context.Detallespedidosproductos.ToListAsync()) :
                          Problem("Entity set 'AlexasoftContext.Detallespedidosproductos'  is null.");
        }

        // GET: DetallesPedidosProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Detallespedidosproductos == null)
            {
                return NotFound();
            }

            var detallePedidoProducto = await _context.Detallespedidosproductos
                .Include(d => d.IdPedidoNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetallePedidoProducto == id);
            if (detallePedidoProducto == null)
            {
                return NotFound();
            }

            return View(detallePedidoProducto);
        }

        // GET: DetallesPedidosProductos/Create
        public IActionResult Create()
        {
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre");
            return View();
        }

        // POST: DetallesPedidosProductos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetallePedidoProducto,IdPedido,IdProducto,UnidadesXproducto,PrecioUnitario")] Detallespedidosproducto detallespedidosproducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallespedidosproducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", detallespedidosproducto.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", detallespedidosproducto.IdProducto);
            return View(detallespedidosproducto);
        }

        // GET: DetallesPedidosProductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Detallespedidosproductos == null)
            {
                return NotFound();
            }

            var detallePedidoProducto = await _context.Detallespedidosproductos.FindAsync(id);
            if (detallePedidoProducto == null)
            {
                return NotFound();
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", detallePedidoProducto.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", detallePedidoProducto.IdProducto);
            return View(detallePedidoProducto);
        }

        // POST: DetallesPedidosProductos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetallePedidoProducto,IdPedido,IdProducto,UnidadesXproducto,PrecioUnitario")] Detallespedidosproducto detallespedidosproducto)
        {
            if (id != detallespedidosproducto.IdDetallePedidoProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallespedidosproducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallespedidosproductoExists(detallespedidosproducto.IdDetallePedidoProducto))
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
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", detallespedidosproducto.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", detallespedidosproducto.IdProducto);
            return View(detallespedidosproducto);
        }

        // GET: DetallesPedidosProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Detallespedidosproductos == null)
            {
                return NotFound();
            }

            var detallePedidoProducto = await _context.Detallespedidosproductos
                .Include(d => d.IdPedidoNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetallePedidoProducto == id);
            if (detallePedidoProducto == null)
            {
                return NotFound();
            }

            return View(detallePedidoProducto);
        }

        // POST: DetallesPedidosProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Detallespedidosproductos == null)
            {
                return Problem("Entity set 'AlexasoftContext.Detallespedidosproductos'  is null.");
            }
            var detallePedidoProducto = await _context.Detallespedidosproductos.FindAsync(id);
            if (detallePedidoProducto != null)
            {
                _context.Detallespedidosproductos.Remove(detallePedidoProducto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallespedidosproductoExists(int id)
        {
            return (_context.Detallespedidosproductos?.Any(e => e.IdDetallePedidoProducto == id)).GetValueOrDefault();
        }
    }
}
