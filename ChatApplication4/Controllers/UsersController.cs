using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChatApplication4.Models;

namespace ChatApplication4.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.CustomUsers != null ? 
                          View(await _context.CustomUsers.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.CustomUsers'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomUsers == null)
            {
                return NotFound();
            }

            var customUser = await _context.CustomUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customUser == null)
            {
                return NotFound();
            }

            return View(customUser);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Role,ProfilePicture,Gender")] CustomUser customUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customUser);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomUsers == null)
            {
                return NotFound();
            }

            var customUser = await _context.CustomUsers.FindAsync(id);
            if (customUser == null)
            {
                return NotFound();
            }
            return View(customUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Role,ProfilePicture,Gender")] CustomUser customUser)
        {
            if (id != customUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomUserExists(customUser.Id))
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
            return View(customUser);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomUsers == null)
            {
                return NotFound();
            }

            var customUser = await _context.CustomUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customUser == null)
            {
                return NotFound();
            }

            return View(customUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustomUsers == null)
            {
                return Problem("Entity set 'AppDbContext.CustomUsers'  is null.");
            }
            var customUser = await _context.CustomUsers.FindAsync(id);
            if (customUser != null)
            {
                _context.CustomUsers.Remove(customUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetUserName()
        {
            // Fetch the user's name from your data source (e.g., database)
            var userName = _context.CustomUsers.ToList();
            ViewBag.Country = TempData["Country"] as string;
            ViewBag.Region = TempData["Region"] as string;
            ViewBag.City = TempData["City"] as string;

            var onlineUserName=userName.Select(x=>x.Username).ToList();
            var onlineUserGender = userName.Select(x => x.Gender).ToList();

            // Replace this with your actual data retrieval code

            return Json(new { UserName = onlineUserName , Gender= onlineUserGender });
        }


        private bool CustomUserExists(int id)
        {
          return (_context.CustomUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
