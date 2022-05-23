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
    public class TVinylTypesController : Controller
    {
        private readonly db_musicContext _context;

        public TVinylTypesController(db_musicContext context)
        {
            _context = context;
        }

        // GET: TVinylTypes
        public async Task<IActionResult> Index()
        {
              return _context.TVinylTypes != null ? 
                          View(await _context.TVinylTypes.ToListAsync()) :
                          Problem("Entity set 'db_musicContext.TVinylTypes'  is null.");
        }

        // GET: TVinylTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TVinylTypes == null)
            {
                return NotFound();
            }

            var tVinylType = await _context.TVinylTypes
                .FirstOrDefaultAsync(m => m.IdVinylType == id);
            if (tVinylType == null)
            {
                return NotFound();
            }

            return View(tVinylType);
        }

        // GET: TVinylTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TVinylTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVinylType,Name")] TVinylType tVinylType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tVinylType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tVinylType);
        }

        // GET: TVinylTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TVinylTypes == null)
            {
                return NotFound();
            }

            var tVinylType = await _context.TVinylTypes.FindAsync(id);
            if (tVinylType == null)
            {
                return NotFound();
            }
            return View(tVinylType);
        }

        // POST: TVinylTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVinylType,Name")] TVinylType tVinylType)
        {
            if (id != tVinylType.IdVinylType)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tVinylType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TVinylTypeExists(tVinylType.IdVinylType))
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
            return View(tVinylType);
        }

        // GET: TVinylTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TVinylTypes == null)
            {
                return NotFound();
            }

            var tVinylType = await _context.TVinylTypes
                .FirstOrDefaultAsync(m => m.IdVinylType == id);
            if (tVinylType == null)
            {
                return NotFound();
            }

            return View(tVinylType);
        }

        // POST: TVinylTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TVinylTypes == null)
            {
                return Problem("Entity set 'db_musicContext.TVinylTypes'  is null.");
            }
            var tVinylType = await _context.TVinylTypes.FindAsync(id);
            if (tVinylType != null)
            {
                _context.TVinylTypes.Remove(tVinylType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TVinylTypeExists(int id)
        {
          return (_context.TVinylTypes?.Any(e => e.IdVinylType == id)).GetValueOrDefault();
        }
    }
}
