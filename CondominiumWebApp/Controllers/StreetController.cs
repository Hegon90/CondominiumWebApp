using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CondominiumWebApp.Models;

namespace CondominiumWebApp.Controllers
{
    public class StreetController : Controller
    {
        private readonly CondominiumContext _context;

        public StreetController(CondominiumContext context)
        {
            _context = context;
        }

        // GET: Street
        public async Task<IActionResult> Index()
        {
              return _context.Streets != null ? 
                          View(await _context.Streets.ToListAsync()) :
                          Problem("Entity set 'CondominiumContext.Streets'  is null.");
        }

        // GET: Street/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Streets == null)
            {
                return NotFound();
            }

            var street = await _context.Streets
                .FirstOrDefaultAsync(m => m.StreetId == id);
            if (street == null)
            {
                return NotFound();
            }

            return View(street);
        }

        // GET: Street/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Street/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StreetId,StreetNumber")] Street street)
        {
            if (ModelState.IsValid)
            {
                _context.Add(street);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(street);
        }

        // GET: Street/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Streets == null)
            {
                return NotFound();
            }

            var street = await _context.Streets.FindAsync(id);
            if (street == null)
            {
                return NotFound();
            }
            return View(street);
        }

        // POST: Street/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StreetId,StreetNumber")] Street street)
        {
            if (id != street.StreetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(street);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StreetExists(street.StreetId))
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
            return View(street);
        }

        // GET: Street/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Streets == null)
            {
                return NotFound();
            }

            var street = await _context.Streets
                .FirstOrDefaultAsync(m => m.StreetId == id);
            if (street == null)
            {
                return NotFound();
            }

            return View(street);
        }

        // POST: Street/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Streets == null)
            {
                return Problem("Entity set 'CondominiumContext.Streets'  is null.");
            }
            var street = await _context.Streets.FindAsync(id);
            if (street != null)
            {
                _context.Streets.Remove(street);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StreetExists(int id)
        {
          return (_context.Streets?.Any(e => e.StreetId == id)).GetValueOrDefault();
        }
    }
}
