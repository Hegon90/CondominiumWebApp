using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CondominiumWebApp.Models;

namespace CondominiumWebApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly CondominiumContext _context;

        public BookingController(CondominiumContext context)
        {
            _context = context;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
              return _context.Bookings != null ? 
                          View(await _context.Bookings.ToListAsync()) :
                          Problem("Entity set 'CondominiumContext.Bookings'  is null.");
        }

        // GET: Booking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Booking/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,BookingPlace,BookingDay,BookingTime,BookingName")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                // Check for duplicates
                bool isDuplicate = await _context.Bookings.AnyAsync(b =>
                    b.BookingPlace == booking.BookingPlace &&
                    b.BookingDay == booking.BookingDay &&
                    b.BookingTime == booking.BookingTime);

                if (isDuplicate)
                {
                    ModelState.AddModelError(string.Empty, "Area no disponible en esta fecha y hora.");
                    return View(booking);
                }

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(booking);
        }

        // GET: Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,BookingPlace,BookingDay,BookingTime,BookingName")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Check for duplicates
                bool isDuplicate = await _context.Bookings.AnyAsync(b =>
                    b.BookingId != booking.BookingId &&
                    b.BookingPlace == booking.BookingPlace &&
                    b.BookingDay == booking.BookingDay &&
                    b.BookingTime == booking.BookingTime);

                if (isDuplicate)
                {
                    ModelState.AddModelError(string.Empty, "Area no disponible en esta fecha y hora.");
                    return View(booking);
                }

                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
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

            return View(booking);
        }


        //// POST: Booking/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("BookingId,BookingPlace,BookingDay,BookingTime,BookingName")] Booking booking)
        //{
        //    if (id != booking.BookingId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(booking);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BookingExists(booking.BookingId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(booking);
        //}

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bookings == null)
            {
                return Problem("Entity set 'CondominiumContext.Bookings'  is null.");
            }
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
          return (_context.Bookings?.Any(e => e.BookingId == id)).GetValueOrDefault();
        }
    }
}
