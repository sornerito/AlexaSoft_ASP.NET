﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlexaSoft_ASP.NET.Models;
using System.Data;
using AlexaSoft_ASP.NET.Utilities;

namespace AlexaSoft_ASP.NET.Controllers
{
    public class ComprasController : Controller
    {
        private readonly AlexasoftContext _context;

        public ComprasController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Compras"))
            {
                return RedirectToAction("Error", "Home");
            }
            var alexasoftContext = _context.Compras.Include(c => c.IdProveedorNavigation);
            return View(await alexasoftContext.ToListAsync());
        }

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Compras"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdProveedorNavigation)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // GET: Compras/Create
        public IActionResult Create()

        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Compras"))
            {
                return RedirectToAction("Error", "Home");
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "Nombre");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre");

            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompra,IdProveedor,Precio,Fecha,Subtotal,MotivoAnular")] Compra compra, int idProducto, int Unidades, int idDetalles)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Compras"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                int compraSaved = await _context.SaveChangesAsync();
                if (Unidades == null)
                {
                    ModelState.AddModelError("Ingrese una cantidad", "Ingrese una cantidad.");
                    ;
                    return View(compra);
                }

                if (compraSaved > 0)
                {
                    int i = Guid.NewGuid().GetHashCode();


                    Detallesproductosxcompra detallesproductosxcompra = new Detallesproductosxcompra
                    {
                        IdDetalleProductoXcompra = i,
                        IdCompra = compra.IdCompra,
                        IdProducto = idProducto,
                        Unidades = Unidades
                    };

                    _context.Detallesproductosxcompras.Add(detallesproductosxcompra);
                    await _context.SaveChangesAsync();
                }
                var producto = await _context.Productos.FindAsync(idProducto);
                if (producto != null)
                {
                    producto.Unidades += Unidades;
                    _context.Productos.Update(producto);
                    await _context.SaveChangesAsync();
                }


                return RedirectToAction(nameof(Index));

            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", compra.IdProveedor);



            return View(compra);
        }

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Compras"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", compra.IdProveedor);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCompra,IdProveedor,Precio,Fecha,Subtotal,MotivoAnular")] Compra compra)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Compras"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id != compra.IdCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(compra.MotivoAnular))
                    {
                        var detallesCompra = await _context.Detallesproductosxcompras.Where(d => d.IdCompra == compra.IdCompra).ToListAsync();
                        if (detallesCompra != null)
                        {
                            // Restar las unidades de los productos correspondientes
                            foreach (var detalle in detallesCompra)
                            {
                                var producto = await _context.Productos.FindAsync(detalle.IdProducto);
                                if (producto != null)
                                {
                                    producto.Unidades -= detalle.Unidades;
                                    _context.Update(producto);
                                }
                            }
                        }
                    }
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.IdCompra))
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
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", compra.IdProveedor);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Compras"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdProveedorNavigation)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Compras"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (_context.Compras == null)
            {
                return Problem("Entity set 'AlexasoftContext.Compras'  is null.");
            }
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
          return (_context.Compras?.Any(e => e.IdCompra == id)).GetValueOrDefault();
        }
    }
}
