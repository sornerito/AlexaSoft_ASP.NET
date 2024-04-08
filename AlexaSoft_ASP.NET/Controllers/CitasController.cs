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
    public class CitasController : Controller
    {
        private readonly AlexasoftContext _context;

        public CitasController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var alexasoftContext = _context.Citas.Include(c => c.IdClienteNavigation).Include(c => c.IdColaboradorNavigation).Include(c => c.IdHorarioNavigation).Include(c => c.IdMotivoCancelacionNavigation).Include(c => c.IdPaqueteNavigation);
            return View(await alexasoftContext.ToListAsync());
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Citas == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdColaboradorNavigation)
                .Include(c => c.IdHorarioNavigation)
                .Include(c => c.IdMotivoCancelacionNavigation)
                .Include(c => c.IdPaqueteNavigation)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {

            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre");
            var colaboradoresActivos = _context.Colaboradores.Where(c => c.Estado == "Activo").ToList();
            var colaboradoresSelectList = colaboradoresActivos.Select(c => new SelectListItem
            {
                Value = c.IdColaborador.ToString(),
                Text = c.Nombre
            }).ToList();
            ViewData["IdColaborador"] = new SelectList(colaboradoresSelectList, "Value", "Text"); ;
            var horarioActivos = _context.Horarios.Where(h => h.Estado == "Activo").ToList();


            var horarioSelectList = horarioActivos.Select(h => new SelectListItem
            {
                Value = h.IdHorario.ToString(),
                Text = ConvertirNumeroDiaANombre(h.NumeroDia)
            }).ToList();

            ViewData["IdHorario"] = new SelectList(horarioSelectList, "Value", "Text");

            ViewData["IdMotivoCancelacion"] = new SelectList(_context.Motivoscancelacions, "IdMotivo", "Motivo");
            ViewData["IdPaquete"] = new SelectList(_context.Paquetes, "IdPaquete", "Nombre");
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,Fecha,Hora,Detalle,Estado,IdMotivoCancelacion,IdCliente,IdPaquete,IdHorario,IdColaborador")] Cita cita)
        {
            
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Citas == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre", cita.IdCliente);
            var colaboradoresActivos = _context.Colaboradores.Where(c => c.Estado == "Activo").ToList();


            var colaboradoresSelectList = colaboradoresActivos.Select(c => new SelectListItem
            {
                Value = c.IdColaborador.ToString(),
                Text = c.Nombre
            }).ToList();

            ViewData["IdColaborador"] = new SelectList(colaboradoresSelectList, "Value", "Text"); ;
            var horarioActivos = _context.Horarios.Where(h => h.Estado == "Activo").ToList();


            var horarioSelectList = horarioActivos.Select(h => new SelectListItem
            {
                Value = h.IdHorario.ToString(),
                Text = ConvertirNumeroDiaANombre(h.NumeroDia)
            }).ToList();

            ViewData["IdHorario"] = new SelectList(horarioSelectList, "Value", "Text");
            ViewData["IdMotivoCancelacion"] = new SelectList(_context.Motivoscancelacions, "IdMotivo", "Motivo", cita.IdMotivoCancelacion);
            ViewData["IdPaquete"] = new SelectList(_context.Paquetes, "IdPaquete", "Nombre", cita.IdPaquete);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCita,Fecha,Hora,Detalle,Estado,IdMotivoCancelacion,IdCliente,IdPaquete,IdHorario,IdColaborador")] Cita cita)
        {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.IdCita))
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

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Citas == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdColaboradorNavigation)
                .Include(c => c.IdHorarioNavigation)
                .Include(c => c.IdMotivoCancelacionNavigation)
                .Include(c => c.IdPaqueteNavigation)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Citas == null)
            {
                return Problem("Entity set 'AlexasoftContext.Citas'  is null.");
            }
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
          return (_context.Citas?.Any(e => e.IdCita == id)).GetValueOrDefault();
        }

        private string ConvertirNumeroDiaANombre(int numeroDia)
        {
            switch (numeroDia)
            {
                case 0:
                    return "Lunes";
                case 1:
                    return "Martes";
                case 2:
                    return "Miércoles";
                case 3:
                    return "Jueves";
                case 4:
                    return "Viernes";
                case 5:
                    return "Sábado";
                case 6:
                    return "Domingo";
                default:
                    return "";
            }
        }
    }
}
