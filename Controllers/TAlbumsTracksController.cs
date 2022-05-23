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
    public class TAlbumsTracksController : Controller
    {
        private readonly db_musicContext _context;

        public TAlbumsTracksController(db_musicContext context)
        {
            _context = context;
        }

        // GET: TAlbumsTracks
        public async Task<IActionResult> Index()
        {
            var db_musicContext = _context.TAlbumsTracks.Include(t => t.IdAlbumNavigation).Include(t => t.IdTracksNavigation);
            return View(await db_musicContext.ToListAsync());
        }

        // GET: TAlbumsTracks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TAlbumsTracks == null)
            {
                return NotFound();
            }

            var tAlbumsTrack = await _context.TAlbumsTracks
                .Include(t => t.IdAlbumNavigation)
                .Include(t => t.IdTracksNavigation)
                .FirstOrDefaultAsync(m => m.IdAlbum == id);
            if (tAlbumsTrack == null)
            {
                return NotFound();
            }

            return View(tAlbumsTrack);
        }

        // GET: TAlbumsTracks/Create
        public IActionResult Create()
        {
            ViewData["IdAlbum"] = new SelectList(_context.TAlbums, "IdAlbum", "IdAlbum");
            ViewData["IdTracks"] = new SelectList(_context.TTracks, "IdTracks", "IdTracks");
            return View();
        }

        // POST: TAlbumsTracks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAlbum,IdTracks,TrackNumber")] TAlbumsTrack tAlbumsTrack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tAlbumsTrack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlbum"] = new SelectList(_context.TAlbums, "IdAlbum", "IdAlbum", tAlbumsTrack.IdAlbum);
            ViewData["IdTracks"] = new SelectList(_context.TTracks, "IdTracks", "IdTracks", tAlbumsTrack.IdTracks);
            return View(tAlbumsTrack);
        }

        // GET: TAlbumsTracks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TAlbumsTracks == null)
            {
                return NotFound();
            }

            var tAlbumsTrack = await _context.TAlbumsTracks.FindAsync(id);
            if (tAlbumsTrack == null)
            {
                return NotFound();
            }
            ViewData["IdAlbum"] = new SelectList(_context.TAlbums, "IdAlbum", "IdAlbum", tAlbumsTrack.IdAlbum);
            ViewData["IdTracks"] = new SelectList(_context.TTracks, "IdTracks", "IdTracks", tAlbumsTrack.IdTracks);
            return View(tAlbumsTrack);
        }

        // POST: TAlbumsTracks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAlbum,IdTracks,TrackNumber")] TAlbumsTrack tAlbumsTrack)
        {
            if (id != tAlbumsTrack.IdAlbum)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tAlbumsTrack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TAlbumsTrackExists(tAlbumsTrack.IdAlbum))
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
            ViewData["IdAlbum"] = new SelectList(_context.TAlbums, "IdAlbum", "IdAlbum", tAlbumsTrack.IdAlbum);
            ViewData["IdTracks"] = new SelectList(_context.TTracks, "IdTracks", "IdTracks", tAlbumsTrack.IdTracks);
            return View(tAlbumsTrack);
        }

        // GET: TAlbumsTracks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TAlbumsTracks == null)
            {
                return NotFound();
            }

            var tAlbumsTrack = await _context.TAlbumsTracks
                .Include(t => t.IdAlbumNavigation)
                .Include(t => t.IdTracksNavigation)
                .FirstOrDefaultAsync(m => m.IdAlbum == id);
            if (tAlbumsTrack == null)
            {
                return NotFound();
            }

            return View(tAlbumsTrack);
        }

        // POST: TAlbumsTracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TAlbumsTracks == null)
            {
                return Problem("Entity set 'db_musicContext.TAlbumsTracks'  is null.");
            }
            var tAlbumsTrack = await _context.TAlbumsTracks.FindAsync(id);
            if (tAlbumsTrack != null)
            {
                _context.TAlbumsTracks.Remove(tAlbumsTrack);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TAlbumsTrackExists(int id)
        {
          return (_context.TAlbumsTracks?.Any(e => e.IdAlbum == id)).GetValueOrDefault();
        }
    }
}
