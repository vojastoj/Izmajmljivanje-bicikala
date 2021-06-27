using Iznajmljivanje_bicikala.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iznajmljivanje_bicikala.Controllers
{    [Authorize (Roles = "Admin")] 
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());


        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bicycle = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bicycle == null)
            {
                return NotFound();
            }

            return View(bicycle);
        }

        // POST: Bicycles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var bicycle = await _context.Users.FindAsync(id);
            _context.Users.Remove(bicycle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
