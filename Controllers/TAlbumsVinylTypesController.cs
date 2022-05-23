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
    public class TAlbumsVinylTypesController : Controller
    {
        private readonly db_musicContext _context;

        public TAlbumsVinylTypesController(db_musicContext context)
        {
            _context = context;
        }

        // GET: TAlbumsVinylTypes
        public async Task<IActionResult> Index()
        {
            var db_musicContext = _context.TAlbumsVinylTypes.Include(t => t.IdAlbumNavigation).Include(t => t.IdVinylTypeNavigation);
            return View(await db_musicContext.ToListAsync());
        }

        // GET: TAlbumsVinylTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TAlbumsVinylTypes == null)
            {
                return NotFound();
            }

            var tAlbumsVinylType = await _context.TAlbumsVinylTypes
                .Include(t => t.IdAlbumNavigation)
                .Include(t => t.IdVinylTypeNavigation)
                .FirstOrDefaultAsync(m => m.IdAlbum == id);
            if (tAlbumsVinylType == null)
            {
                return NotFound();
            }

            return View(tAlbumsVinylType);
        }

        // GET: TAlbumsVinylTypes/Create
        public IActionResult Create()
        {
            ViewData["IdAlbum"] = new SelectList(_context.TAlbums, "IdAlbum", "IdAlbum");
            ViewData["IdVinylType"] = new SelectList(_context.TVinylTypes, "IdVinylType", "IdVinylType");
            return View();
        }

        // POST: TAlbumsVinylTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAlbum,IdVinylType,Picture,ReleaseDate,Price,Stock")] TAlbumsVinylType tAlbumsVinylType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tAlbumsVinylType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlbum"] = new SelectList(_context.TAlbums, "IdAlbum", "IdAlbum", tAlbumsVinylType.IdAlbum);
            ViewData["IdVinylType"] = new SelectList(_context.TVinylTypes, "IdVinylType", "IdVinylType", tAlbumsVinylType.IdVinylType);
            return View(tAlbumsVinylType);
        }

        // GET: TAlbumsVinylTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TAlbumsVinylTypes == null)
            {
                return NotFound();
            }

            var tAlbumsVinylType = await _context.TAlbumsVinylTypes.FindAsync(id);
            if (tAlbumsVinylType == null)
            {
                return NotFound();
            }
            ViewData["IdAlbum"] = new SelectList(_context.TAlbums, "IdAlbum", "IdAlbum", tAlbumsVinylType.IdAlbum);
            ViewData["IdVinylType"] = new SelectList(_context.TVinylTypes, "IdVinylType", "IdVinylType", tAlbumsVinylType.IdVinylType);
            return View(tAlbumsVinylType);
        }

        // POST: TAlbumsVinylTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAlbum,IdVinylType,Picture,ReleaseDate,Price,Stock")] TAlbumsVinylType tAlbumsVinylType)
        {
            if (id != tAlbumsVinylType.IdAlbum)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tAlbumsVinylType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TAlbumsVinylTypeExists(tAlbumsVinylType.IdAlbum))
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
            ViewData["IdAlbum"] = new SelectList(_context.TAlbums, "IdAlbum", "IdAlbum", tAlbumsVinylType.IdAlbum);
            ViewData["IdVinylType"] = new SelectList(_context.TVinylTypes, "IdVinylType", "IdVinylType", tAlbumsVinylType.IdVinylType);
            return View(tAlbumsVinylType);
        }

        // GET: TAlbumsVinylTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TAlbumsVinylTypes == null)
            {
                return NotFound();
            }

            var tAlbumsVinylType = await _context.TAlbumsVinylTypes
                .Include(t => t.IdAlbumNavigation)
                .Include(t => t.IdVinylTypeNavigation)
                .FirstOrDefaultAsync(m => m.IdAlbum == id);
            if (tAlbumsVinylType == null)
            {
                return NotFound();
            }

            return View(tAlbumsVinylType);
        }

        // POST: TAlbumsVinylTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TAlbumsVinylTypes == null)
            {
                return Problem("Entity set 'db_musicContext.TAlbumsVinylTypes'  is null.");
            }
            var tAlbumsVinylType = await _context.TAlbumsVinylTypes.FindAsync(id);
            if (tAlbumsVinylType != null)
            {
                _context.TAlbumsVinylTypes.Remove(tAlbumsVinylType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TAlbumsVinylTypeExists(int id)
        {
          return (_context.TAlbumsVinylTypes?.Any(e => e.IdAlbum == id)).GetValueOrDefault();
        }
    }
}
