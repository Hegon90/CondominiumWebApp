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

        // GET: Property/Create
        public IActionResult Create()
        {
            ViewData["BlockId"] = new SelectList(_context.Blocks, "BlockId", "BlockName");
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerFullName");
            ViewData["StreetId"] = new SelectList(_context.Streets, "StreetId", "StreetNumber");
            return View();
        }

        // POST: PropertyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropertyId,PropertyPasscode,PropertyType,BlockId,StreetId,OwnerId,PropertyDate,PropertyNumber")] Property property)
        {
            ModelState.Remove("Block");
            ModelState.Remove("Street");

            if (ModelState.IsValid)
            {
                // Check for duplicates
                bool isDuplicate = await _context.Properties.AnyAsync(p => p.PropertyPasscode == property.PropertyPasscode);

                if (isDuplicate)
                {
                    ModelState.AddModelError(nameof(property.PropertyPasscode), "Este codigo ya existe.");
                    ViewData["BlockId"] = new SelectList(_context.Blocks, "BlockId", "BlockName", property.BlockId);
                    ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerFullName", property.OwnerId);
                    ViewData["StreetId"] = new SelectList(_context.Streets, "StreetId", "StreetNumber", property.StreetId);
                    return View(property);
                }

                // Retrieve associated Block and Street entities
                var block = await _context.Blocks.FindAsync(property.BlockId);
                var street = await _context.Streets.FindAsync(property.StreetId);

                // Create the new property
                var propertyToAdd = new Property
                {
                    PropertyPasscode = property.PropertyPasscode,
                    PropertyType = property.PropertyType,
                    BlockId = property.BlockId,
                    StreetId = property.StreetId,
                    PropertyNumber = property.PropertyNumber,
                    OwnerId = property.OwnerId,
                    PropertyDate = property.PropertyDate,
                };

                _context.Add(propertyToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BlockId"] = new SelectList(_context.Blocks, "BlockId", "BlockName", property.BlockId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerFullName", property.OwnerId);
            ViewData["StreetId"] = new SelectList(_context.Streets, "StreetId", "StreetNumber", property.StreetId);
            return View(property);
        }

        //GET: Property/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            var viewModel = new PropertyEditViewModel
            {
                PropertyId = property.PropertyId,
                PropertyPasscode = property.PropertyPasscode,
                PropertyType = property.PropertyType,
                OwnerId = property.OwnerId,
                PropertyDate = property.PropertyDate
            };

            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerFullName", viewModel.OwnerId);

            return View(viewModel);
        }

        //POST: Property/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PropertyEditViewModel viewModel)
        {
            if (id != viewModel.PropertyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var property = await _context.Properties.FindAsync(id);
                    if (property == null)
                    {
                        return NotFound();
                    }

                    // Update the specific properties you want to modify
                    property.PropertyPasscode = viewModel.PropertyPasscode;
                    property.PropertyType = viewModel.PropertyType;
                    property.OwnerId = viewModel.OwnerId;
                    property.PropertyDate = viewModel.PropertyDate;

                    // Save the changes to the database
                    _context.Update(property);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyExists(viewModel.PropertyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerFullName", viewModel.OwnerId);

            return View(viewModel);
        }


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
