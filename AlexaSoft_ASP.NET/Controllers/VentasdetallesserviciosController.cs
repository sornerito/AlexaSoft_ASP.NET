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
    public class VentasdetallesserviciosController : Controller
    {
        private readonly AlexasoftContext _context;

        public VentasdetallesserviciosController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: Ventasdetallesservicios
        public async Task<IActionResult> Index()
        {
            var alexasoftContext = _context.Ventasdetallesservicios.Include(v => v.IdPaqueteNavigation).Include(v => v.IdVentaNavigation);
            return View(await alexasoftContext.ToListAsync());
        }

        // GET: Ventasdetallesservicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ventasdetallesservicios == null)
            {
                return NotFound();
            }

            var ventasdetallesservicio = await _context.Ventasdetallesservicios
                .Include(v => v.IdPaqueteNavigation)
                .Include(v => v.IdVentaNavigation)
                .FirstOrDefaultAsync(m => m.IdVentaDetalleServicio == id);
            if (ventasdetallesservicio == null)
            {
                return NotFound();
            }

            return View(ventasdetallesservicio);
        }

        // GET: Ventasdetallesservicios/Create
        public IActionResult Create()
        {
            ViewData["IdPaquete"] = new SelectList(_context.Paquetes, "IdPaquete", "IdPaquete");
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta");
            return View();
        }

        // POST: Ventasdetallesservicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVentaDetalleServicio,IdVenta,IdPaquete,Cantidad,Subtotal")] Ventasdetallesservicio ventasdetallesservicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventasdetallesservicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPaquete"] = new SelectList(_context.Paquetes, "IdPaquete", "IdPaquete", ventasdetallesservicio.IdPaquete);
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", ventasdetallesservicio.IdVenta);
            return View(ventasdetallesservicio);
        }

        // GET: Ventasdetallesservicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ventasdetallesservicios == null)
            {
                return NotFound();
            }

            var ventasdetallesservicio = await _context.Ventasdetallesservicios.FindAsync(id);
            if (ventasdetallesservicio == null)
            {
                return NotFound();
            }
            ViewData["IdPaquete"] = new SelectList(_context.Paquetes, "IdPaquete", "IdPaquete", ventasdetallesservicio.IdPaquete);
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", ventasdetallesservicio.IdVenta);
            return View(ventasdetallesservicio);
        }

        // POST: Ventasdetallesservicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVentaDetalleServicio,IdVenta,IdPaquete,Cantidad,Subtotal")] Ventasdetallesservicio ventasdetallesservicio)
        {
            if (id != ventasdetallesservicio.IdVentaDetalleServicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventasdetallesservicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentasdetallesservicioExists(ventasdetallesservicio.IdVentaDetalleServicio))
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
            ViewData["IdPaquete"] = new SelectList(_context.Paquetes, "IdPaquete", "IdPaquete", ventasdetallesservicio.IdPaquete);
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", ventasdetallesservicio.IdVenta);
            return View(ventasdetallesservicio);
        }

        // GET: Ventasdetallesservicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ventasdetallesservicios == null)
            {
                return NotFound();
            }

            var ventasdetallesservicio = await _context.Ventasdetallesservicios
                .Include(v => v.IdPaqueteNavigation)
                .Include(v => v.IdVentaNavigation)
                .FirstOrDefaultAsync(m => m.IdVentaDetalleServicio == id);
            if (ventasdetallesservicio == null)
            {
                return NotFound();
            }

            return View(ventasdetallesservicio);
        }

        // POST: Ventasdetallesservicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ventasdetallesservicios == null)
            {
                return Problem("Entity set 'AlexasoftContext.Ventasdetallesservicios'  is null.");
            }
            var ventasdetallesservicio = await _context.Ventasdetallesservicios.FindAsync(id);
            if (ventasdetallesservicio != null)
            {
                _context.Ventasdetallesservicios.Remove(ventasdetallesservicio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentasdetallesservicioExists(int id)
        {
          return (_context.Ventasdetallesservicios?.Any(e => e.IdVentaDetalleServicio == id)).GetValueOrDefault();
        }
    }
}
