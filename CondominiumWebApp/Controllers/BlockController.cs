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
    public class BlockController : Controller
    {
        private readonly CondominiumContext _context;

        public BlockController(CondominiumContext context)
        {
            _context = context;
        }

        // GET: Block
        public async Task<IActionResult> Index()
        {
              return _context.Blocks != null ? 
                          View(await _context.Blocks.ToListAsync()) :
                          Problem("Entity set 'CondominiumContext.Blocks'  is null.");
        }

        // GET: Block/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Blocks == null)
            {
                return NotFound();
            }

            var block = await _context.Blocks
                .FirstOrDefaultAsync(m => m.BlockId == id);
            if (block == null)
            {
                return NotFound();
            }

            return View(block);
        }

        // GET: Block/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Block/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlockId,BlockName")] Block block)
        {
            if (ModelState.IsValid)
            {
                _context.Add(block);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(block);
        }

        // GET: Block/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Blocks == null)
            {
                return NotFound();
            }

            var block = await _context.Blocks.FindAsync(id);
            if (block == null)
            {
                return NotFound();
            }
            return View(block);
        }

        // POST: Block/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlockId,BlockName")] Block block)
        {
            if (id != block.BlockId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(block);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlockExists(block.BlockId))
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
            return View(block);
        }

        // GET: Block/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Blocks == null)
            {
                return NotFound();
            }

            var block = await _context.Blocks
                .FirstOrDefaultAsync(m => m.BlockId == id);
            if (block == null)
            {
                return NotFound();
            }

            return View(block);
        }

        // POST: Block/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Blocks == null)
            {
                return Problem("Entity set 'CondominiumContext.Blocks'  is null.");
            }
            var block = await _context.Blocks.FindAsync(id);
            if (block != null)
            {
                _context.Blocks.Remove(block);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlockExists(int id)
        {
          return (_context.Blocks?.Any(e => e.BlockId == id)).GetValueOrDefault();
        }
    }
}
