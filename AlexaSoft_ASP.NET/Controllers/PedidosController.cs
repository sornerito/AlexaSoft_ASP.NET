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
    public class PedidosController : Controller
    {
        private readonly AlexasoftContext _context;

        public PedidosController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var pedidos = await _context.Pedidos
                        .Include(p => p.IdClienteNavigation)
                        .Include(p => p.IdColaboradorNavigation)
                        .OrderBy(p => p.Estado == "Pendiente" ? 0 : p.Estado == "Aceptado" ? 1 : 2)
                        .ToListAsync();

            return View(pedidos);
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.IdClienteNavigation)
                .Include(p => p.IdColaboradorNavigation)
                .FirstOrDefaultAsync(m => m.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre");
            ViewData["IdColaborador"] = new SelectList(_context.Colaboradores, "IdColaborador", "Nombre");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPedido,FechaCreacion,FechaFinalizacion,Estado,Total,Iva,IdCliente,IdColaborador")] Pedido pedido)
        {
            if (!ModelState.IsValid)
            {
                pedido.Estado = "Pendiente";
                pedido.Iva = 19;
                pedido.FechaCreacion = DateTime.Now;

                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre", pedido.IdCliente);
            ViewData["IdColaborador"] = new SelectList(_context.Colaboradores, "IdColaborador", "Nombre", pedido.IdColaborador);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre", pedido.IdCliente);
            ViewData["IdColaborador"] = new SelectList(_context.Colaboradores, "IdColaborador", "Nombre", pedido.IdColaborador);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPedido,FechaCreacion,FechaFinalizacion,Estado,Total,Iva,IdCliente,IdColaborador")] Pedido pedido)
        {
            if (id != pedido.IdPedido)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    pedido.Iva = 19;
                    _context.Update(pedido);

                    // Establecer la fecha de finalización si el estado es "Aceptado" o "Cancelado"
                    if (pedido.Estado == "Aceptado" || pedido.Estado == "Cancelado")
                    {
                        pedido.FechaFinalizacion = DateTime.Now;
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.IdPedido))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre", pedido.IdCliente);
            ViewData["IdColaborador"] = new SelectList(_context.Colaboradores, "IdColaborador", "Nombre", pedido.IdColaborador);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.IdClienteNavigation)
                .Include(p => p.IdColaboradorNavigation)
                .FirstOrDefaultAsync(m => m.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'AlexasoftContext.Pedidos'  is null.");
            }
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
          return (_context.Pedidos?.Any(e => e.IdPedido == id)).GetValueOrDefault();
        }
    }
}
