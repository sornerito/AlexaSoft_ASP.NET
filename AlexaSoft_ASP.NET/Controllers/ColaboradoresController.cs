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
    public class ColaboradoresController : Controller
    {
        private readonly AlexasoftContext _context;

        public ColaboradoresController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: Colaboradores
        public async Task<IActionResult> Index()
        {
            var alexasoftContext = _context.Colaboradores.Include(c => c.IdRolNavigation);
            return View(await alexasoftContext.ToListAsync());
        }


        // GET: Colaboradores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Colaboradores == null)
            {
                return NotFound();
            }

            var colaboradore = await _context.Colaboradores
                .Include(c => c.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdColaborador == id);
            if (colaboradore == null)
            {
                return NotFound();
            }

            return View(colaboradore);
        }

        // GET: Colaboradores/Create
        public IActionResult Create()
        {

            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "Nombre");
            return View();
        }


        // POST: Colaboradores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("IdColaborador,Nombre,Cedula,Correo,Telefono,Contrasena,Estado,IdRol")] Colaboradore colaboradore)
        {
            
                _context.Add(colaboradore);
                await _context.SaveChangesAsync();
                
            return RedirectToAction(nameof(Index));
        }

        // GET: Colaboradores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Colaboradores == null)
            {
                return NotFound();
            }

            var colaboradore = await _context.Colaboradores.FindAsync(id);
            if (colaboradore == null)
            {
                return NotFound();
            }
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "Nombre", colaboradore.IdRol);
            return View(colaboradore);
        }

        // POST: Colaboradores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdColaborador,Nombre,Cedula,Correo,Telefono,Contrasena,Estado,FechaInteraccion,IdRol")] Colaboradore colaboradore)
        {
        

            
                try
                {
                    _context.Update(colaboradore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColaboradoreExists(colaboradore.IdColaborador))
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

        // GET: Colaboradores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Colaboradores == null)
            {
                return NotFound();
            }

            var colaboradore = await _context.Colaboradores
                .Include(c => c.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdColaborador == id);
            if (colaboradore == null)
            {
                return NotFound();
            }

            return View(colaboradore);
        }

        // POST: Colaboradores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Colaboradores == null)
            {
                return Problem("Entity set 'AlexasoftContext.Colaboradores'  is null.");
            }
            var colaboradore = await _context.Colaboradores.FindAsync(id);
            if (colaboradore != null)
            {
                _context.Colaboradores.Remove(colaboradore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColaboradoreExists(int id)
        {
          return (_context.Colaboradores?.Any(e => e.IdColaborador == id)).GetValueOrDefault();
        }
    }
}
