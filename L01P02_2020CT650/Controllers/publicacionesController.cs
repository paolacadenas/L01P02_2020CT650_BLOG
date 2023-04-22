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
    public class publicacionesController : Controller
    {
        private readonly blogDbContext _context;

        public publicacionesController(blogDbContext context)
        {
            _context = context;
        }

        // GET: publicaciones
        public async Task<IActionResult> Index()
        {
              return _context.publicaciones != null ? 
                          View(await _context.publicaciones.ToListAsync()) :
                          Problem("Entity set 'blogDbContext.publicaciones'  is null.");
        }

        // GET: publicaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.publicaciones == null)
            {
                return NotFound();
            }

            var publicaciones = await _context.publicaciones
                .FirstOrDefaultAsync(m => m.publicacionId == id);
            if (publicaciones == null)
            {
                return NotFound();
            }

            return View(publicaciones);
        }

        // GET: publicaciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: publicaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("publicacionId,titulo,descripcion,usuarioId")] publicaciones publicaciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publicaciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publicaciones);
        }

        // GET: publicaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.publicaciones == null)
            {
                return NotFound();
            }

            var publicaciones = await _context.publicaciones.FindAsync(id);
            if (publicaciones == null)
            {
                return NotFound();
            }
            return View(publicaciones);
        }

        // POST: publicaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("publicacionId,titulo,descripcion,usuarioId")] publicaciones publicaciones)
        {
            if (id != publicaciones.publicacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicaciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!publicacionesExists(publicaciones.publicacionId))
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
            return View(publicaciones);
        }

        // GET: publicaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.publicaciones == null)
            {
                return NotFound();
            }

            var publicaciones = await _context.publicaciones
                .FirstOrDefaultAsync(m => m.publicacionId == id);
            if (publicaciones == null)
            {
                return NotFound();
            }

            return View(publicaciones);
        }

        // POST: publicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.publicaciones == null)
            {
                return Problem("Entity set 'blogDbContext.publicaciones'  is null.");
            }
            var publicaciones = await _context.publicaciones.FindAsync(id);
            if (publicaciones != null)
            {
                _context.publicaciones.Remove(publicaciones);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool publicacionesExists(int id)
        {
          return (_context.publicaciones?.Any(e => e.publicacionId == id)).GetValueOrDefault();
        }
    }
}
