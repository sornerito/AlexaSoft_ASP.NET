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
    public class ProductosController : Controller
    {
        private readonly AlexasoftContext _context;

        public ProductosController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Productos"))
            {
                return RedirectToAction("Error", "Home");
            }
            var alexasoftContext = _context.Productos.Include(p => p.IdCategoriaProductoNavigation);
            return View(await alexasoftContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Productos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Productos"))
            {
                return RedirectToAction("Error", "Home");
            }
            ViewData["IdCategoriaProducto"] = new SelectList(_context.CategoriaProductos, "IdCategoriaProducto", "Nombre");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Marca,Precio,Unidades,Estado,IdCategoriaProducto")] Producto producto)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Productos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoriaProducto"] = new SelectList(_context.CategoriaProductos, "IdCategoriaProducto", "IdCategoriaProducto", producto.IdCategoriaProducto);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Productos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoriaProducto"] = new SelectList(_context.CategoriaProductos, "IdCategoriaProducto", "IdCategoriaProducto", producto.IdCategoriaProducto);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,Nombre,Marca,Precio,Unidades,Estado,IdCategoriaProducto")] Producto producto)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Productos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
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
            ViewData["IdCategoriaProducto"] = new SelectList(_context.CategoriaProductos, "IdCategoriaProducto", "IdCategoriaProducto", producto.IdCategoriaProducto);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Productos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Productos"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (_context.Productos == null)
            {
                return Problem("Entity set 'AlexasoftContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return (_context.Productos?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }
    }
}
