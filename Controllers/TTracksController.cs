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
    public class TTracksController : Controller
    {
        private readonly db_musicContext _context;

        public TTracksController(db_musicContext context)
        {
            _context = context;
        }

        // GET: TTracks
        public async Task<IActionResult> Index()
        {
              return _context.TTracks != null ? 
                          View(await _context.TTracks.ToListAsync()) :
                          Problem("Entity set 'db_musicContext.TTracks'  is null.");
        }

        // GET: TTracks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TTracks == null)
            {
                return NotFound();
            }

            var tTrack = await _context.TTracks
                .FirstOrDefaultAsync(m => m.IdTracks == id);
            if (tTrack == null)
            {
                return NotFound();
            }

            return View(tTrack);
        }

        // GET: TTracks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TTracks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTracks,TrackName,DurationSec")] TTrack tTrack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tTrack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tTrack);
        }

        // GET: TTracks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TTracks == null)
            {
                return NotFound();
            }

            var tTrack = await _context.TTracks.FindAsync(id);
            if (tTrack == null)
            {
                return NotFound();
            }
            return View(tTrack);
        }

        // POST: TTracks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTracks,TrackName,DurationSec")] TTrack tTrack)
        {
            if (id != tTrack.IdTracks)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tTrack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TTrackExists(tTrack.IdTracks))
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
            return View(tTrack);
        }

        // GET: TTracks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TTracks == null)
            {
                return NotFound();
            }

            var tTrack = await _context.TTracks
                .FirstOrDefaultAsync(m => m.IdTracks == id);
            if (tTrack == null)
            {
                return NotFound();
            }

            return View(tTrack);
        }

        // POST: TTracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TTracks == null)
            {
                return Problem("Entity set 'db_musicContext.TTracks'  is null.");
            }
            var tTrack = await _context.TTracks.FindAsync(id);
            if (tTrack != null)
            {
                _context.TTracks.Remove(tTrack);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TTrackExists(int id)
        {
          return (_context.TTracks?.Any(e => e.IdTracks == id)).GetValueOrDefault();
        }
    }
}
