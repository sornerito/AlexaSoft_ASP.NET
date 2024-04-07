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
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
            var alexasoftContext = _context.Ventasdetalleproductos.Include(v => v.IdProductoNavigation).Include(v => v.IdVentaNavigation);
            return View(await alexasoftContext.ToListAsync());
        }

        // GET: Ventasdetalleproductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
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
        public async Task<IActionResult> CreateAsync()
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre");
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta");

            var productos = await _context.Productos.ToListAsync();
            var precios = productos.ToDictionary(p => p.IdProducto, p => p.Precio);

            ViewBag.Precios = precios;

            return View();
        }

        // POST: Ventasdetalleproductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVentaDetalleProducto,IdVenta,IdProducto,PrecioUnitario,Cantidad")] Ventasdetalleproducto ventasdetalleproducto)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (!ModelState.IsValid)
            {
                var producto = await _context.Productos.FindAsync(ventasdetalleproducto.IdProducto);

                if (producto != null)
                {
                    if (ventasdetalleproducto.Cantidad > producto.Unidades)
                    {
                        ModelState.AddModelError("Cantidad", "La cantidad deseada excede la cantidad disponible.");
                        ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", ventasdetalleproducto.IdProducto);
                        return View(ventasdetalleproducto);
                    }

                    producto.Unidades -= ventasdetalleproducto.Cantidad;
                    _context.Update(producto);

                }

                _context.Add(ventasdetalleproducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", ventasdetalleproducto.IdProducto);
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", ventasdetalleproducto.IdVenta);
            return View(ventasdetalleproducto);
        }

        // GET: Ventasdetalleproductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.Ventasdetalleproductos == null)
            {
                return NotFound();
            }

            var ventasdetalleproducto = await _context.Ventasdetalleproductos.FindAsync(id);
            if (ventasdetalleproducto == null)
            {
                return NotFound();
            }
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", ventasdetalleproducto.IdVenta);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", ventasdetalleproducto.IdProducto);

            var productos = await _context.Productos.ToListAsync();
            var precios = productos.ToDictionary(p => p.IdProducto, p => p.Precio);

            ViewBag.Precios = precios;

            return View(ventasdetalleproducto);
        }

        // POST: Ventasdetalleproductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVentaDetalleProducto,IdVenta,IdProducto,PrecioUnitario,Cantidad")] Ventasdetalleproducto ventasdetalleproducto)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id != ventasdetalleproducto.IdVentaDetalleProducto)
            {
                return NotFound();
            }

            var originalDetalle = await _context.Ventasdetalleproductos.AsNoTracking().FirstOrDefaultAsync(d => d.IdVentaDetalleProducto == id);
            if (originalDetalle == null)
            {
                return NotFound();
            }
            var productoOriginal = await _context.Productos.FindAsync(originalDetalle.IdProducto);
            if (productoOriginal == null)
            {
                return NotFound();
            }
            var productoNuevo = await _context.Productos.FindAsync(ventasdetalleproducto.IdProducto);
            if (productoNuevo == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    productoOriginal.Unidades += originalDetalle.Cantidad;
                    if (ventasdetalleproducto.Cantidad > productoNuevo.Unidades)
                    {
                        ModelState.AddModelError("Cantidad", "La modificación excede la cantidad disponible del producto seleccionado.");
                        ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", ventasdetalleproducto.IdVenta);
                        ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", ventasdetalleproducto.IdProducto);
                        return View(ventasdetalleproducto);
                    }

                    productoNuevo.Unidades -= ventasdetalleproducto.Cantidad;

                    _context.Update(productoOriginal);
                    _context.Update(productoNuevo);
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
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", ventasdetalleproducto.IdVenta);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", ventasdetalleproducto.IdProducto);
            return View(ventasdetalleproducto);
        }

        // GET: Ventasdetalleproductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
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
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
            var detallesventasproducto = await _context.Ventasdetalleproductos.FindAsync(id);
            if (detallesventasproducto == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(detallesventasproducto.IdProducto);
            if (producto != null)
            {
                producto.Unidades += detallesventasproducto.Cantidad;
                _context.Update(producto);
            }

            _context.Ventasdetalleproductos.Remove(detallesventasproducto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool VentasdetalleproductoExists(int id)
        {
          return (_context.Ventasdetalleproductos?.Any(e => e.IdVentaDetalleProducto == id)).GetValueOrDefault();
        }
    }
}
