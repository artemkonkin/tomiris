using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tomiris.Models;

namespace tomiris.Controllers
{
    public class ShoppingListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingList
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShoppingLists.Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ShoppingList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingListModel = await _context.ShoppingLists
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.id == id);
            if (shoppingListModel == null)
            {
                return NotFound();
            }

            return View(shoppingListModel);
        }

        // GET: ShoppingList/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ShoppingList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,UserId")] ShoppingListModel shoppingListModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingListModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", shoppingListModel.UserId);
            return View(shoppingListModel);
        }

        // GET: ShoppingList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingListModel = await _context.ShoppingLists.FindAsync(id);
            if (shoppingListModel == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", shoppingListModel.UserId);
            return View(shoppingListModel);
        }

        // POST: ShoppingList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,UserId")] ShoppingListModel shoppingListModel)
        {
            if (id != shoppingListModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingListModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingListModelExists(shoppingListModel.id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", shoppingListModel.UserId);
            return View(shoppingListModel);
        }

        // GET: ShoppingList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingListModel = await _context.ShoppingLists
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.id == id);
            if (shoppingListModel == null)
            {
                return NotFound();
            }

            return View(shoppingListModel);
        }

        // POST: ShoppingList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingListModel = await _context.ShoppingLists.FindAsync(id);
            _context.ShoppingLists.Remove(shoppingListModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingListModelExists(int id)
        {
            return _context.ShoppingLists.Any(e => e.id == id);
        }
    }
}
