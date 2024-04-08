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
    public class VentasController : Controller
    {
        private readonly AlexasoftContext _context;

        public VentasController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
            var alexasoftContext = _context.Ventas.Include(v => v.IdColaboradorNavigation).Include(v => v.IdPedidoNavigation).Include(v => v.IdUsuarioNavigation);
            return View(await alexasoftContext.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.IdColaboradorNavigation)
                .Include(v => v.IdPedidoNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
            var colaboradoresActivos = _context.Colaboradores.Where(p => p.Estado == "Activo").ToList();


            var colaboradoresSelectList = colaboradoresActivos.Select(p => new SelectListItem
            {
                Value = p.IdColaborador.ToString(),
                Text = p.Nombre
            }).ToList();


            ViewData["IdColaborador"] = new SelectList(colaboradoresSelectList, "Value", "Text");
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVenta,NumeroFactura,IdPedido,IdColaborador,Fecha,MotivoAnular,IdUsuario,Total,Iva")] Venta venta)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (!ModelState.IsValid)
            {
                // Generar el número de factura automáticamente
                venta.NumeroFactura = GenerateInvoiceNumber();
                venta.Iva = 19;
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdColaborador"] = new SelectList(_context.Colaboradores, "IdColaborador", "IdColaborador", venta.IdColaborador);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", venta.IdPedido);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", venta.IdUsuario);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            ViewData["IdColaborador"] = new SelectList(_context.Colaboradores, "IdColaborador", "IdColaborador", venta.IdColaborador);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", venta.IdPedido);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", venta.IdUsuario);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVenta,NumeroFactura,IdPedido,IdColaborador,Fecha,MotivoAnular,IdUsuario,Total,Iva")] Venta venta)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id != venta.IdVenta)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {   
                    venta.Iva = 19;
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.IdVenta))
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
            ViewData["IdColaborador"] = new SelectList(_context.Colaboradores, "IdColaborador", "IdColaborador", venta.IdColaborador);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", venta.IdPedido);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", venta.IdUsuario);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.IdColaboradorNavigation)
                .Include(v => v.IdPedidoNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!AccesoHelper.TienePermiso(HttpContext, "Gestionar Ventas"))
            {
                return RedirectToAction("Error", "Home");
            }
            if (_context.Ventas == null)
            {
                return Problem("Entity set 'AlexasoftContext.Ventas'  is null.");
            }
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
          return (_context.Ventas?.Any(e => e.IdVenta == id)).GetValueOrDefault();
        }

        // Método para generar el número de factura
        private string GenerateInvoiceNumber()
        {
            string prefix = "FACT-" + DateTime.Now.Year.ToString() + "-";
            string lastInvoiceNumber = _context.Ventas.OrderBy(v => v.IdVenta).Select(v => v.NumeroFactura).LastOrDefault();
            int lastNumber = 0;

            if (!string.IsNullOrEmpty(lastInvoiceNumber))
            {
                // Extraer el número de factura anterior y aumentarlo en uno
                string lastNumberStr = lastInvoiceNumber.Split('-').Last();
                if (int.TryParse(lastNumberStr, out lastNumber))
                {
                    lastNumber++;
                }
            }

            string invoiceNumber = prefix + lastNumber.ToString().PadLeft(3, '0');
            return invoiceNumber;
        }

    }
}
