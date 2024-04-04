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
    public class DetallespedidosserviciosController : Controller
    {
        private readonly AlexasoftContext _context;

        public DetallespedidosserviciosController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: Detallespedidosservicios
        public async Task<IActionResult> Index()
        {
            var alexasoftContext = _context.Detallespedidosservicios.Include(d => d.IdPaqueteNavigation).Include(d => d.IdPedidoNavigation);
            return View(await alexasoftContext.ToListAsync());
        }

        // GET: Detallespedidosservicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Detallespedidosservicios == null)
            {
                return NotFound();
            }

            var detallespedidosservicio = await _context.Detallespedidosservicios
                .Include(d => d.IdPaqueteNavigation)
                .Include(d => d.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetallePedidoServicio == id);
            if (detallespedidosservicio == null)
            {
                return NotFound();
            }

            return View(detallespedidosservicio);
        }

        // GET: Detallespedidosservicios/Create
        public IActionResult Create()
        {
            ViewData["IdPaquete"] = new SelectList(_context.Paquetes, "IdPaquete", "IdPaquete");
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido");
            return View();
        }

        // POST: Detallespedidosservicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetallePedidoServicio,IdPaquete,IdPedido,Precio")] Detallespedidosservicio detallespedidosservicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallespedidosservicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPaquete"] = new SelectList(_context.Paquetes, "IdPaquete", "IdPaquete", detallespedidosservicio.IdPaquete);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", detallespedidosservicio.IdPedido);
            return View(detallespedidosservicio);
        }

        // GET: Detallespedidosservicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Detallespedidosservicios == null)
            {
                return NotFound();
            }

            var detallespedidosservicio = await _context.Detallespedidosservicios.FindAsync(id);
            if (detallespedidosservicio == null)
            {
                return NotFound();
            }
            ViewData["IdPaquete"] = new SelectList(_context.Paquetes, "IdPaquete", "IdPaquete", detallespedidosservicio.IdPaquete);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", detallespedidosservicio.IdPedido);
            return View(detallespedidosservicio);
        }

        // POST: Detallespedidosservicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetallePedidoServicio,IdPaquete,IdPedido,Precio")] Detallespedidosservicio detallespedidosservicio)
        {
            if (id != detallespedidosservicio.IdDetallePedidoServicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallespedidosservicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallespedidosservicioExists(detallespedidosservicio.IdDetallePedidoServicio))
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
            ViewData["IdPaquete"] = new SelectList(_context.Paquetes, "IdPaquete", "IdPaquete", detallespedidosservicio.IdPaquete);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", detallespedidosservicio.IdPedido);
            return View(detallespedidosservicio);
        }

        // GET: Detallespedidosservicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Detallespedidosservicios == null)
            {
                return NotFound();
            }

            var detallespedidosservicio = await _context.Detallespedidosservicios
                .Include(d => d.IdPaqueteNavigation)
                .Include(d => d.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetallePedidoServicio == id);
            if (detallespedidosservicio == null)
            {
                return NotFound();
            }

            return View(detallespedidosservicio);
        }

        // POST: Detallespedidosservicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Detallespedidosservicios == null)
            {
                return Problem("Entity set 'AlexasoftContext.Detallespedidosservicios'  is null.");
            }
            var detallespedidosservicio = await _context.Detallespedidosservicios.FindAsync(id);
            if (detallespedidosservicio != null)
            {
                _context.Detallespedidosservicios.Remove(detallespedidosservicio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallespedidosservicioExists(int id)
        {
          return (_context.Detallespedidosservicios?.Any(e => e.IdDetallePedidoServicio == id)).GetValueOrDefault();
        }
    }
}
