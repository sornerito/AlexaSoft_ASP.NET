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
    public class DetallespedidosproductosController : Controller
    {
        private readonly AlexasoftContext _context;

        public DetallespedidosproductosController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: Detallespedidosproductos
        public async Task<IActionResult> Index()
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Pedidos"))
            {
                return RedirectToAction("Error", "Home");
            }
            var alexasoftContext = _context.Detallespedidosproductos.Include(d => d.IdPedidoNavigation).Include(d => d.IdProductoNavigation);
            return View(await alexasoftContext.ToListAsync());
        }

        // GET: Detallespedidosproductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Pedidos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.Detallespedidosproductos == null)
            {
                return NotFound();
            }

            var detallespedidosproducto = await _context.Detallespedidosproductos
                .Include(d => d.IdPedidoNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetallePedidoProducto == id);
            if (detallespedidosproducto == null)
            {
                return NotFound();
            }

            return View(detallespedidosproducto);
        }

        // GET: Detallespedidosproductos/Create
        public IActionResult Create()
        {
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            return View();
        }

        // POST: Detallespedidosproductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetallePedidoProducto,IdPedido,IdProducto,UnidadesXproducto,PrecioUnitario")] Detallespedidosproducto detallespedidosproducto)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Pedidos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (!ModelState.IsValid)
            {

                var producto = await _context.Productos.FindAsync(detallespedidosproducto.IdProducto);

                if (producto != null)
                {
                    if (detallespedidosproducto.UnidadesXproducto > producto.Unidades)
                    {
                        ModelState.AddModelError("Cantidad", "La cantidad deseada excede la cantidad disponible.");
                        ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", detallespedidosproducto.IdProducto);
                        return View(detallespedidosproducto);
                    }

                    producto.Unidades -= detallespedidosproducto.UnidadesXproducto;
                    _context.Update(producto);
                
                }
            _context.Add(detallespedidosproducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", detallespedidosproducto.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detallespedidosproducto.IdProducto);
            return View(detallespedidosproducto);
        }

        // GET: Detallespedidosproductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Pedidos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.Detallespedidosproductos == null)
            {
                return NotFound();
            }

            var detallespedidosproducto = await _context.Detallespedidosproductos.FindAsync(id);
            if (detallespedidosproducto == null)
            {
                return NotFound();
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", detallespedidosproducto.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detallespedidosproducto.IdProducto);
            return View(detallespedidosproducto);
        }

        // POST: Detallespedidosproductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetallePedidoProducto,IdPedido,IdProducto,UnidadesXproducto,PrecioUnitario")] Detallespedidosproducto detallespedidosproducto)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Pedidos"))
            {
                return RedirectToAction("Error", "Home");
            }
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detallespedidosproducto.IdProducto);
            return View(detallespedidosproducto);
        }

        // GET: Detallespedidosproductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Pedidos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.Detallespedidosproductos == null)
            {
                return NotFound();
            }

            var detallespedidosproducto = await _context.Detallespedidosproductos
                .Include(d => d.IdPedidoNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetallePedidoProducto == id);
            if (detallespedidosproducto == null)
            {
                return NotFound();
            }

            return View(detallespedidosproducto);
        }

        // POST: Detallespedidosproductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Pedidos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (_context.Detallespedidosproductos == null)
            {
                return Problem("Entity set 'AlexasoftContext.Detallespedidosproductos'  is null.");
            }
            var detallespedidosproducto = await _context.Detallespedidosproductos.FindAsync(id);
            if (detallespedidosproducto != null)
            {
                _context.Detallespedidosproductos.Remove(detallespedidosproducto);
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
