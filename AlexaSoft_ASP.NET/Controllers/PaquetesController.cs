﻿using System;
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
    public class PaquetesController : Controller
    {
        private readonly AlexasoftContext _context;

        public PaquetesController(AlexasoftContext context)
        {
            _context = context;
        }

        // GET: Paquetes
        public async Task<IActionResult> Index()
        {
            
            var paquetesconServicios = _context.Paquetes
            .Include(p => p.PaquetesServicios)
                .ThenInclude(Ps => Ps.IdServicioNavigation)
            .ToList();

            return View(paquetesconServicios);
        }

        // GET: Paquetes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Paquetes == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquetes
                .FirstOrDefaultAsync(m => m.IdPaquete == id);
            if (paquete == null)
            {
                return NotFound();
            }

            return View(paquete);
        }

        // GET: Paquetes/Create
        public IActionResult Create()
        {
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "Nombre");

            return View();
        }

        // POST: Paquetes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPaquete,Nombre,Descripcion,Estado")] Paquete paquete, int idServicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paquete);
                int compraSaved = await _context.SaveChangesAsync();

                if (compraSaved > 0)
                {

                    PaquetesServicio paquetesServicio = new PaquetesServicio
                    {
                        IdPaquete = paquete.IdPaquete,
                        IdServicio = idServicio,
                    };

                    _context.PaquetesServicios.Add(paquetesServicio);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));

            }

            return View(paquete);
        }


    // GET: Paquetes/Edit/5
    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Paquetes == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquetes.FindAsync(id);
            if (paquete == null)
            {
                return NotFound();
            }
            return View(paquete);
        }

        // POST: Paquetes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPaquete,Nombre,Descripcion,Estado")] Paquete paquete)
        {
            if (id != paquete.IdPaquete)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paquete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaqueteExists(paquete.IdPaquete))
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
            return View(paquete);
        }

        // GET: Paquetes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Paquetes == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquetes
                .FirstOrDefaultAsync(m => m.IdPaquete == id);
            if (paquete == null)
            {
                return NotFound();
            }

            return View(paquete);
        }

        // POST: Paquetes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Paquetes == null)
            {
                return Problem("Entity set 'AlexasoftContext.Paquetes'  is null.");
            }
            var paquete = await _context.Paquetes.FindAsync(id);
            if (paquete != null)
            {
                _context.Paquetes.Remove(paquete);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaqueteExists(int id)
        {
          return (_context.Paquetes?.Any(e => e.IdPaquete == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarEstado(int idpaquete)
        {
           
            var rol = await _context.Paquetes.FindAsync(idpaquete);
            if (rol == null)
            {
                return NotFound();
            }

            rol.Estado = rol.Estado == "Activo" ? "Desactivado" : "Activo";
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
