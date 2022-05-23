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
    public class TLabelsController : Controller
    {
        private readonly db_musicContext _context;

        public TLabelsController(db_musicContext context)
        {
            _context = context;
        }

        // GET: TLabels
        public async Task<IActionResult> Index()
        {
              return _context.TLabels != null ? 
                          View(await _context.TLabels.ToListAsync()) :
                          Problem("Entity set 'db_musicContext.TLabels'  is null.");
        }

        // GET: TLabels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TLabels == null)
            {
                return NotFound();
            }

            var tLabel = await _context.TLabels
                .FirstOrDefaultAsync(m => m.IdLabel == id);
            if (tLabel == null)
            {
                return NotFound();
            }

            return View(tLabel);
        }

        // GET: TLabels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TLabels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLabel,Name")] TLabel tLabel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tLabel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tLabel);
        }

        // GET: TLabels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TLabels == null)
            {
                return NotFound();
            }

            var tLabel = await _context.TLabels.FindAsync(id);
            if (tLabel == null)
            {
                return NotFound();
            }
            return View(tLabel);
        }

        // POST: TLabels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLabel,Name")] TLabel tLabel)
        {
            if (id != tLabel.IdLabel)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tLabel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TLabelExists(tLabel.IdLabel))
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
            return View(tLabel);
        }

        // GET: TLabels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TLabels == null)
            {
                return NotFound();
            }

            var tLabel = await _context.TLabels
                .FirstOrDefaultAsync(m => m.IdLabel == id);
            if (tLabel == null)
            {
                return NotFound();
            }

            return View(tLabel);
        }

        // POST: TLabels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TLabels == null)
            {
                return Problem("Entity set 'db_musicContext.TLabels'  is null.");
            }
            var tLabel = await _context.TLabels.FindAsync(id);
            if (tLabel != null)
            {
                _context.TLabels.Remove(tLabel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TLabelExists(int id)
        {
          return (_context.TLabels?.Any(e => e.IdLabel == id)).GetValueOrDefault();
        }
    }
}
