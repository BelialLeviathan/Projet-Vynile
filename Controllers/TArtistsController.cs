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
    public class TArtistsController : Controller
    {
        private readonly db_musicContext _context;

        public TArtistsController(db_musicContext context)
        {
            _context = context;
        }

        // GET: TArtists
        public async Task<IActionResult> Index()
        {
              return _context.TArtists != null ? 
                          View(await _context.TArtists.ToListAsync()) :
                          Problem("Entity set 'db_musicContext.TArtists'  is null.");
        }

        // GET: TArtists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TArtists == null)
            {
                return NotFound();
            }

            var tArtist = await _context.TArtists
                .FirstOrDefaultAsync(m => m.IdArtist == id);
            if (tArtist == null)
            {
                return NotFound();
            }

            return View(tArtist);
        }

        // GET: TArtists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TArtists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdArtist,Lastname,Firstname,Pseudonym,Birtdate,Deathdate")] TArtist tArtist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tArtist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tArtist);
        }

        // GET: TArtists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TArtists == null)
            {
                return NotFound();
            }

            var tArtist = await _context.TArtists.FindAsync(id);
            if (tArtist == null)
            {
                return NotFound();
            }
            return View(tArtist);
        }

        // POST: TArtists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdArtist,Lastname,Firstname,Pseudonym,Birtdate,Deathdate")] TArtist tArtist)
        {
            if (id != tArtist.IdArtist)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tArtist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TArtistExists(tArtist.IdArtist))
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
            return View(tArtist);
        }

        // GET: TArtists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TArtists == null)
            {
                return NotFound();
            }

            var tArtist = await _context.TArtists
                .FirstOrDefaultAsync(m => m.IdArtist == id);
            if (tArtist == null)
            {
                return NotFound();
            }

            return View(tArtist);
        }

        // POST: TArtists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TArtists == null)
            {
                return Problem("Entity set 'db_musicContext.TArtists'  is null.");
            }
            var tArtist = await _context.TArtists.FindAsync(id);
            if (tArtist != null)
            {
                _context.TArtists.Remove(tArtist);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TArtistExists(int id)
        {
          return (_context.TArtists?.Any(e => e.IdArtist == id)).GetValueOrDefault();
        }
    }
}
