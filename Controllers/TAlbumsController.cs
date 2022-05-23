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
    public class TAlbumsController : Controller
    {
        private readonly db_musicContext _context;

        public TAlbumsController(db_musicContext context)
        {
            _context = context;
        }

        // GET: TAlbums
        public async Task<IActionResult> Index()
        {
            var db_musicContext = _context.TAlbums.Include(t => t.IdLabelNavigation);
            return View(await db_musicContext.ToListAsync());
        }

        // GET: TAlbums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TAlbums == null)
            {
                return NotFound();
            }

            var tAlbum = await _context.TAlbums
                .Include(t => t.IdLabelNavigation)
                .FirstOrDefaultAsync(m => m.IdAlbum == id);
            if (tAlbum == null)
            {
                return NotFound();
            }

            return View(tAlbum);
        }

        // GET: TAlbums/Create
        public IActionResult Create()
        {
            ViewData["IdLabel"] = new SelectList(_context.TLabels, "IdLabel", "IdLabel");
            return View();
        }

        // POST: TAlbums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAlbum,IdLabel,Title")] TAlbum tAlbum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tAlbum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLabel"] = new SelectList(_context.TLabels, "IdLabel", "IdLabel", tAlbum.IdLabel);
            return View(tAlbum);
        }

        // GET: TAlbums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TAlbums == null)
            {
                return NotFound();
            }

            var tAlbum = await _context.TAlbums.FindAsync(id);
            if (tAlbum == null)
            {
                return NotFound();
            }
            ViewData["IdLabel"] = new SelectList(_context.TLabels, "IdLabel", "IdLabel", tAlbum.IdLabel);
            return View(tAlbum);
        }

        // POST: TAlbums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAlbum,IdLabel,Title")] TAlbum tAlbum)
        {
            if (id != tAlbum.IdAlbum)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tAlbum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TAlbumExists(tAlbum.IdAlbum))
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
            ViewData["IdLabel"] = new SelectList(_context.TLabels, "IdLabel", "IdLabel", tAlbum.IdLabel);
            return View(tAlbum);
        }

        // GET: TAlbums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TAlbums == null)
            {
                return NotFound();
            }

            var tAlbum = await _context.TAlbums
                .Include(t => t.IdLabelNavigation)
                .FirstOrDefaultAsync(m => m.IdAlbum == id);
            if (tAlbum == null)
            {
                return NotFound();
            }

            return View(tAlbum);
        }

        // POST: TAlbums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TAlbums == null)
            {
                return Problem("Entity set 'db_musicContext.TAlbums'  is null.");
            }
            var tAlbum = await _context.TAlbums.FindAsync(id);
            if (tAlbum != null)
            {
                _context.TAlbums.Remove(tAlbum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TAlbumExists(int id)
        {
          return (_context.TAlbums?.Any(e => e.IdAlbum == id)).GetValueOrDefault();
        }
    }
}
