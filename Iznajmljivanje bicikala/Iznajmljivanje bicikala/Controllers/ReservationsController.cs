using Iznajmljivanje_bicikala.Data;
using Iznajmljivanje_bicikala.Models;
using Iznajmljivanje_bicikala.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Iznajmljivanje_bicikala.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Bicycle> bicycles = new List<Bicycle>();
            if (User.IsInRole("Admin"))
            {
                var bikes = _context.Set<UserBicycle>()
                    .Where(e => e.IsBooked)
                    .Include(e => e.Bicycle)
                    .Select(e => e.Bicycle);
                bicycles.AddRange(bikes);
            }
            else
            {
                var user = await _context.Users
                    .Include(e => e.UserBicycles)
                    .ThenInclude(e => e.Bicycle)
                    .FirstOrDefaultAsync(e => e.Id == userId);
                bicycles.AddRange(user.UserBicycles.Where(e => e.IsBooked).Select(e => e.Bicycle));
            }

            var viewModel = new BicycleViewModel
            {
                Bicycles = bicycles
            };

            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var bikeUser = await _context.Set<UserBicycle>()
                .Include(e => e.Bicycle)
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.BicycleId == id && e.IsBooked);

            return View(bikeUser);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int? id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (id == null)
            {
                return NotFound();
            }

            var bicycle = await _context.Set<UserBicycle>()
                .FirstOrDefaultAsync(m => m.BicycleId == id && m.UserId == userId);
            if (bicycle == null)
            {
                return NotFound();
            }

            return View(bicycle);
        }

        // POST: Bicycles/Delete/5
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var bicycle = await _context.Set<UserBicycle>().FirstOrDefaultAsync(m => m.BicycleId == id && m.UserId == userId);
            bicycle.IsBooked = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}