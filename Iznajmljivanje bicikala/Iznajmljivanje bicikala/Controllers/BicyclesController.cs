using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Iznajmljivanje_bicikala.Data;
using Iznajmljivanje_bicikala.Models;
using System.Security.Claims;
using Iznajmljivanje_bicikala.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Iznajmljivanje_bicikala.Controllers
{
    public class BicyclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BicyclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Booking(int id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == userId);
            var bike = await _context.Bicycles.FirstOrDefaultAsync(e => e.Id == id);

            var bikeUser = await _context.Set<UserBicycle>().FirstOrDefaultAsync(e => e.BicycleId == id && e.UserId == userId);
            if (bikeUser == default)
            {
                bikeUser = new UserBicycle
                {
                    Bicycle = bike,
                    BicycleId = id,
                    User = user,
                    UserId = userId,
                    IsBooked = true
                };

                _context.Set<UserBicycle>().Add(bikeUser);
            }
            else
            {
                bikeUser.IsBooked = true;
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            BicycleViewModel viewModel = new BicycleViewModel
            {
                Bicycles = User.IsInRole("Admin")
                ? await _context.Bicycles.ToListAsync()
                : await _context.Bicycles.Where(e => !_context.Set<UserBicycle>().Any(ub => ub.BicycleId == e.Id && ub.IsBooked)).ToListAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Filter(BicycleViewModel bView)

        {
            BicycleViewModel viewModel = new BicycleViewModel
            {
                Bicycles = await _context.Bicycles.Where(e => e.Brand == bView.Bike.Brand).ToListAsync()
            };
            return View(nameof(Index), viewModel);
        }

        // GET: Bicycles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bicycle = await _context.Bicycles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bicycle == null)
            {
                return NotFound();
            }

            return View(bicycle);
        }

        // GET: Bicycles/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bicycles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,ProductionYear")] Bicycle bicycle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bicycle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bicycle);
        }

        // GET: Bicycles/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bicycle = await _context.Bicycles.FindAsync(id);
            if (bicycle == null)
            {
                return NotFound();
            }
            return View(bicycle);
        }

        // POST: Bicycles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,ProductionYear")] Bicycle bicycle)
        {
            if (id != bicycle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bicycle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BicycleExists(bicycle.Id))
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
            return View(bicycle);
        }

        // GET: Bicycles/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bicycle = await _context.Bicycles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bicycle == null)
            {
                return NotFound();
            }

            return View(bicycle);
        }

        // POST: Bicycles/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bicycle = await _context.Bicycles.FindAsync(id);
            _context.Bicycles.Remove(bicycle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BicycleExists(int id)
        {
            return _context.Bicycles.Any(e => e.Id == id);
        }
    }
}