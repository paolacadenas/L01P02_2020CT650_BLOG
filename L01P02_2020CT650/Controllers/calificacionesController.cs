using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using L01P02_2020CT650.Models;

namespace L01P02_2020CT650.Controllers
{
    public class calificacionesController : Controller
    {
        private readonly blogDbContext _context;

        public calificacionesController(blogDbContext context)
        {
            _context = context;
        }

        // GET: calificaciones
        public async Task<IActionResult> Index()
        {
              return _context.calificaciones != null ? 
                          View(await _context.calificaciones.ToListAsync()) :
                          Problem("Entity set 'blogDbContext.calificaciones'  is null.");
        }

        // GET: calificaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.calificaciones == null)
            {
                return NotFound();
            }

            var calificaciones = await _context.calificaciones
                .FirstOrDefaultAsync(m => m.calificacionId == id);
            if (calificaciones == null)
            {
                return NotFound();
            }

            return View(calificaciones);
        }

        // GET: calificaciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: calificaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("calificacionId,publicacionId,usuarioId,calificacion")] calificaciones calificaciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calificaciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calificaciones);
        }

        // GET: calificaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.calificaciones == null)
            {
                return NotFound();
            }

            var calificaciones = await _context.calificaciones.FindAsync(id);
            if (calificaciones == null)
            {
                return NotFound();
            }
            return View(calificaciones);
        }

        // POST: calificaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("calificacionId,publicacionId,usuarioId,calificacion")] calificaciones calificaciones)
        {
            if (id != calificaciones.calificacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calificaciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!calificacionesExists(calificaciones.calificacionId))
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
            return View(calificaciones);
        }

        // GET: calificaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.calificaciones == null)
            {
                return NotFound();
            }

            var calificaciones = await _context.calificaciones
                .FirstOrDefaultAsync(m => m.calificacionId == id);
            if (calificaciones == null)
            {
                return NotFound();
            }

            return View(calificaciones);
        }

        // POST: calificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.calificaciones == null)
            {
                return Problem("Entity set 'blogDbContext.calificaciones'  is null.");
            }
            var calificaciones = await _context.calificaciones.FindAsync(id);
            if (calificaciones != null)
            {
                _context.calificaciones.Remove(calificaciones);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool calificacionesExists(int id)
        {
          return (_context.calificaciones?.Any(e => e.calificacionId == id)).GetValueOrDefault();
        }
    }
}
