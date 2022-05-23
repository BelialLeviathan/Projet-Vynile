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
    public class TGroupsController : Controller
    {
        private readonly db_musicContext _context;

        public TGroupsController(db_musicContext context)
        {
            _context = context;
        }

        // GET: TGroups
        public async Task<IActionResult> Index()
        {
              return _context.TGroups != null ? 
                          View(await _context.TGroups.ToListAsync()) :
                          Problem("Entity set 'db_musicContext.TGroups'  is null.");
        }

        // GET: TGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TGroups == null)
            {
                return NotFound();
            }

            var tGroup = await _context.TGroups
                .FirstOrDefaultAsync(m => m.IdGroup == id);
            if (tGroup == null)
            {
                return NotFound();
            }

            return View(tGroup);
        }

        // GET: TGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGroup,Name")] TGroup tGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tGroup);
        }

        // GET: TGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TGroups == null)
            {
                return NotFound();
            }

            var tGroup = await _context.TGroups.FindAsync(id);
            if (tGroup == null)
            {
                return NotFound();
            }
            return View(tGroup);
        }

        // POST: TGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGroup,Name")] TGroup tGroup)
        {
            if (id != tGroup.IdGroup)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TGroupExists(tGroup.IdGroup))
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
            return View(tGroup);
        }

        // GET: TGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TGroups == null)
            {
                return NotFound();
            }

            var tGroup = await _context.TGroups
                .FirstOrDefaultAsync(m => m.IdGroup == id);
            if (tGroup == null)
            {
                return NotFound();
            }

            return View(tGroup);
        }

        // POST: TGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TGroups == null)
            {
                return Problem("Entity set 'db_musicContext.TGroups'  is null.");
            }
            var tGroup = await _context.TGroups.FindAsync(id);
            if (tGroup != null)
            {
                _context.TGroups.Remove(tGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TGroupExists(int id)
        {
          return (_context.TGroups?.Any(e => e.IdGroup == id)).GetValueOrDefault();
        }
    }
}
