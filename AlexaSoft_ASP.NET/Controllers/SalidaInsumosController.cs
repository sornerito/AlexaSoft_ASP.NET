using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlexaSoft_ASP.NET.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlexaSoft_ASP.NET.Controllers
{
    public class SalidaInsumoController : Controller
    {
        private readonly AlexasoftContext _context;

        public SalidaInsumoController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: SalidaInsumo
        public async Task<IActionResult> Index() 
        {
            return _context.SalidaInsumos != null ?
                          View(await _context.SalidaInsumos.ToListAsync()) :
                          Problem("Entity set 'AlexasoftContext.SalidaInsumos'  is null.");
        }

        // GET: SalidaInsumo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SalidaInsumos == null)
            {
                return NotFound();
            }

            var salidaInsumo = await _context.SalidaInsumos
                .Include(s => s.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdInsumo == id);
            if (salidaInsumo == null)
            {
                return NotFound();
            }

            return View(salidaInsumo);
        }

        // GET: SalidaInsumo/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre");
            return View();
        }

        // POST: SalidaInsumo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInsumo,IdProducto,FechaRetiro,Cantidad,MotivoAnular")] SalidaInsumo salidaInsumo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salidaInsumo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", salidaInsumo.IdProducto);
            return View(salidaInsumo);
        }

        // GET: SalidaInsumo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SalidaInsumos == null)
            {
                return NotFound();
            }

            var salidaInsumo = await _context.SalidaInsumos.FindAsync(id);
            if (salidaInsumo == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", salidaInsumo.IdProducto);
            return View(salidaInsumo);
        }

        // POST: SalidaInsumo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInsumo,IdProducto,FechaRetiro,Cantidad,MotivoAnular")] SalidaInsumo salidaInsumo)
        {
            if (id != salidaInsumo.IdInsumo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salidaInsumo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalidaInsumoExists(salidaInsumo.IdInsumo))
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", salidaInsumo.IdProducto);
            return View(salidaInsumo);
        }

        // GET: SalidaInsumo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SalidaInsumos == null)
            {
                return NotFound();
            }

            var salidaInsumo = await _context.SalidaInsumos
                .Include(s => s.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdInsumo == id);
            if (salidaInsumo == null)
            {
                return NotFound();
            }

            return View(salidaInsumo);
        }

        // POST: SalidaInsumo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SalidaInsumos == null)
            {
                return Problem("Entity set 'AlexasoftContext.SalidaInsumos'  is null.");
            }
            var salidaInsumo = await _context.SalidaInsumos.FindAsync(id);
            if (salidaInsumo != null)
            {
                _context.SalidaInsumos.Remove(salidaInsumo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalidaInsumoExists(int id)
        {
            return (_context.SalidaInsumos?.Any(e => e.IdInsumo == id)).GetValueOrDefault();
        }
    }
}
