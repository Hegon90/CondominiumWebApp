using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CondominiumWebApp.Models;

namespace CondominiumWebApp.Controllers
{
    public class PropertyController : Controller
    {
        private readonly CondominiumContext _context;

        public PropertyController(CondominiumContext context)
        {
            _context = context;
        }

        // GET: Property
        public async Task<IActionResult> Index()
        {
            var condominiumContext = _context.Properties.Include(p => p.Block).Include(p => p.Owner).Include(p => p.Street);
            return View(await condominiumContext.ToListAsync());
        }

        // GET: Property/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Properties == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .Include(p => p.Block)
                .Include(p => p.Owner)
                .Include(p => p.Street)
                .FirstOrDefaultAsync(m => m.PropertyId == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        //// GET: PropertyController2/Create
        //public IActionResult Create()
        //{
        //    ViewData["BlockId"] = new SelectList(_context.Blocks, "BlockId", "BlockId");
        //    ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId");
        //    ViewData["StreetId"] = new SelectList(_context.Streets, "StreetId", "StreetId");
        //    return View();
        //}

        //// POST: PropertyController2/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("PropertyId,PropertyPasscode,PropertyType,BlockId,StreetId,OwnerId,PropertyDate")] Property @property)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(@property);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BlockId"] = new SelectList(_context.Blocks, "BlockId", "BlockId", @property.BlockId);
        //    ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", @property.OwnerId);
        //    ViewData["StreetId"] = new SelectList(_context.Streets, "StreetId", "StreetId", @property.StreetId);
        //    return View(@property);
        //}


        // GET: Property/Create
        public IActionResult Create()
        {
            ViewData["BlockId"] = new SelectList(_context.Blocks, "BlockId", "BlockName");
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerFullName");
            ViewData["StreetId"] = new SelectList(_context.Streets, "StreetId", "StreetNumber");
            return View();
        }

        //POST: Property/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropertyId,PropertyPasscode,PropertyType,BlockId,StreetId,OwnerId,PropertyDate,PropertyNumber")] Property @property)
        {
            var block = await _context.Blocks.FindAsync(@property.BlockId);
            var street = await _context.Streets.FindAsync(@property.StreetId);

            var propertyToAdd = new Property
            {
                PropertyPasscode = $"{@property.PropertyType.Substring(0, 1)}{block.BlockName}{street.StreetNumber}{@property.PropertyNumber}",
                PropertyType = @property.PropertyType,
                BlockId = @property.BlockId,
                StreetId = @property.StreetId,
                OwnerId = @property.OwnerId,
                PropertyDate = @property.PropertyDate,
            };

            _context.Add(propertyToAdd);
            await _context.SaveChangesAsync();


            ViewData["BlockId"] = new SelectList(_context.Blocks, "BlockId", "BlockName", @property.BlockId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerFullName", @property.OwnerId);
            ViewData["StreetId"] = new SelectList(_context.Streets, "StreetId", "StreetNumber", @property.StreetId);
            return View(@property);
        }


        //// GET: Property/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Properties == null)
        //    {
        //        return NotFound();
        //    }

        //    var @property = await _context.Properties.FindAsync(id);
        //    if (@property == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["BlockId"] = new SelectList(_context.Blocks, "BlockId", "BlockId", @property.BlockId);
        //    ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerFullName", @property.OwnerId);
        //    ViewData["StreetId"] = new SelectList(_context.Streets, "StreetId", "StreetId", @property.StreetId);
        //    return View(@property);
        //}

        //// POST: Property/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("PropertyId,PropertyPasscode,PropertyType,BlockId,StreetId,OwnerId,PropertyDate,PropertyNumber")] Property @property)
        //{
        //    if (id != @property.PropertyId)
        //    {
        //        return NotFound();
        //    }

        //    var block = await _context.Blocks.FindAsync(@property.BlockId);
        //    var street = await _context.Streets.FindAsync(@property.StreetId);

        //    var propertyToAdd = new Property
        //    {
        //        PropertyPasscode = $"{@property.PropertyType.Substring(0, 1)}{block.BlockName}{street.StreetNumber}{@property.PropertyNumber}",
        //        PropertyType = @property.PropertyType,
        //        BlockId = @property.BlockId,
        //        StreetId = @property.StreetId,
        //        OwnerId = @property.OwnerId,
        //        PropertyDate = @property.PropertyDate,
        //    };

        //    try
        //    {
        //        _context.Update(@property);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PropertyExists(@property.PropertyId))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    ViewData["BlockId"] = new SelectList(_context.Blocks, "BlockId", "BlockId", @property.BlockId);
        //    ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerFullName", @property.OwnerId);
        //    ViewData["StreetId"] = new SelectList(_context.Streets, "StreetId", "StreetId", @property.StreetId);
        //    return View(@property);
        //}

        // GET: Property/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Properties == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .Include(p => p.Block)
                .Include(p => p.Owner)
                .Include(p => p.Street)
                .FirstOrDefaultAsync(m => m.PropertyId == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // POST: Property/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Properties == null)
            {
                return Problem("Entity set 'CondominiumContext.Properties'  is null.");
            }
            var @property = await _context.Properties.FindAsync(id);
            if (@property != null)
            {
                _context.Properties.Remove(@property);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyExists(int id)
        {
            return (_context.Properties?.Any(e => e.PropertyId == id)).GetValueOrDefault();
        }
    }
}
