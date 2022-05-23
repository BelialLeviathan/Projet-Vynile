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
    public class TArtistsGroupsController : Controller
    {
        private readonly db_musicContext _context;

        public TArtistsGroupsController(db_musicContext context)
        {
            _context = context;
        }

        // GET: TArtistsGroups
        public async Task<IActionResult> Index()
        {
            var db_musicContext = _context.TArtistsGroups.Include(t => t.IdArtistNavigation).Include(t => t.IdGroupNavigation);
            return View(await db_musicContext.ToListAsync());
        }

        // GET: TArtistsGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TArtistsGroups == null)
            {
                return NotFound();
            }

            var tArtistsGroup = await _context.TArtistsGroups
                .Include(t => t.IdArtistNavigation)
                .Include(t => t.IdGroupNavigation)
                .FirstOrDefaultAsync(m => m.IdArtist == id);
            if (tArtistsGroup == null)
            {
                return NotFound();
            }

            return View(tArtistsGroup);
        }

        // GET: TArtistsGroups/Create
        public IActionResult Create()
        {
            ViewData["IdArtist"] = new SelectList(_context.TArtists, "IdArtist", "IdArtist");
            ViewData["IdGroup"] = new SelectList(_context.TGroups, "IdGroup", "IdGroup");
            return View();
        }

        // POST: TArtistsGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdArtist,IdGroup,IsMember")] TArtistsGroup tArtistsGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tArtistsGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdArtist"] = new SelectList(_context.TArtists, "IdArtist", "IdArtist", tArtistsGroup.IdArtist);
            ViewData["IdGroup"] = new SelectList(_context.TGroups, "IdGroup", "IdGroup", tArtistsGroup.IdGroup);
            return View(tArtistsGroup);
        }

        // GET: TArtistsGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TArtistsGroups == null)
            {
                return NotFound();
            }

            var tArtistsGroup = await _context.TArtistsGroups.FindAsync(id);
            if (tArtistsGroup == null)
            {
                return NotFound();
            }
            ViewData["IdArtist"] = new SelectList(_context.TArtists, "IdArtist", "IdArtist", tArtistsGroup.IdArtist);
            ViewData["IdGroup"] = new SelectList(_context.TGroups, "IdGroup", "IdGroup", tArtistsGroup.IdGroup);
            return View(tArtistsGroup);
        }

        // POST: TArtistsGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdArtist,IdGroup,IsMember")] TArtistsGroup tArtistsGroup)
        {
            if (id != tArtistsGroup.IdArtist)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tArtistsGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TArtistsGroupExists(tArtistsGroup.IdArtist))
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
            ViewData["IdArtist"] = new SelectList(_context.TArtists, "IdArtist", "IdArtist", tArtistsGroup.IdArtist);
            ViewData["IdGroup"] = new SelectList(_context.TGroups, "IdGroup", "IdGroup", tArtistsGroup.IdGroup);
            return View(tArtistsGroup);
        }

        // GET: TArtistsGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TArtistsGroups == null)
            {
                return NotFound();
            }

            var tArtistsGroup = await _context.TArtistsGroups
                .Include(t => t.IdArtistNavigation)
                .Include(t => t.IdGroupNavigation)
                .FirstOrDefaultAsync(m => m.IdArtist == id);
            if (tArtistsGroup == null)
            {
                return NotFound();
            }

            return View(tArtistsGroup);
        }

        // POST: TArtistsGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TArtistsGroups == null)
            {
                return Problem("Entity set 'db_musicContext.TArtistsGroups'  is null.");
            }
            var tArtistsGroup = await _context.TArtistsGroups.FindAsync(id);
            if (tArtistsGroup != null)
            {
                _context.TArtistsGroups.Remove(tArtistsGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TArtistsGroupExists(int id)
        {
          return (_context.TArtistsGroups?.Any(e => e.IdArtist == id)).GetValueOrDefault();
        }
    }
}
