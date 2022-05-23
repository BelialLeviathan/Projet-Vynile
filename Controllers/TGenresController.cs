using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projet_boutique_vinyle.Models;

namespace projet_boutique_vinyle.Controllers
{
    public class TGenresController : Controller
    {
        private readonly db_musicContext _context;

        public TGenresController(db_musicContext context)
        {
            _context = context;
        }

        // GET: TGenres
        public async Task<IActionResult> Index()
        {
              return _context.TGenres != null ? 
                          View(await _context.TGenres.ToListAsync()) :
                          Problem("Entity set 'db_musicContext.TGenres'  is null.");
        }

        // GET: TGenres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TGenres == null)
            {
                return NotFound();
            }

            var tGenre = await _context.TGenres
                .FirstOrDefaultAsync(m => m.IdGenre == id);
            if (tGenre == null)
            {
                return NotFound();
            }

            return View(tGenre);
        }

        // GET: TGenres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGenre,Name")] TGenre tGenre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tGenre);
        }

        // GET: TGenres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TGenres == null)
            {
                return NotFound();
            }

            var tGenre = await _context.TGenres.FindAsync(id);
            if (tGenre == null)
            {
                return NotFound();
            }
            return View(tGenre);
        }

        // POST: TGenres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGenre,Name")] TGenre tGenre)
        {
            if (id != tGenre.IdGenre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TGenreExists(tGenre.IdGenre))
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
            return View(tGenre);
        }

        // GET: TGenres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TGenres == null)
            {
                return NotFound();
            }

            var tGenre = await _context.TGenres
                .FirstOrDefaultAsync(m => m.IdGenre == id);
            if (tGenre == null)
            {
                return NotFound();
            }

            return View(tGenre);
        }

        // POST: TGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TGenres == null)
            {
                return Problem("Entity set 'db_musicContext.TGenres'  is null.");
            }
            var tGenre = await _context.TGenres.FindAsync(id);
            if (tGenre != null)
            {
                _context.TGenres.Remove(tGenre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TGenreExists(int id)
        {
          return (_context.TGenres?.Any(e => e.IdGenre == id)).GetValueOrDefault();
        }
    }
}
