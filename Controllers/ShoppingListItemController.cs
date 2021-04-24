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
    public class ShoppingListItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingListItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingListItem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShoppingListItemModel.Include(s => s.ShoppingList).Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ShoppingListItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingListItemModel = await _context.ShoppingListItemModel
                .Include(s => s.ShoppingList)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.id == id);
            if (shoppingListItemModel == null)
            {
                return NotFound();
            }

            return View(shoppingListItemModel);
        }

        // GET: ShoppingListItem/Create
        public IActionResult Create()
        {
            ViewData["ShoppingListId"] = new SelectList(_context.ShoppingLists, "id", "id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ShoppingListItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Price,Amount,ShoppingListId,UserId")] ShoppingListItemModel shoppingListItemModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingListItemModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShoppingListId"] = new SelectList(_context.ShoppingLists, "id", "id", shoppingListItemModel.ShoppingListId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", shoppingListItemModel.UserId);
            return View(shoppingListItemModel);
        }

        // GET: ShoppingListItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingListItemModel = await _context.ShoppingListItemModel.FindAsync(id);
            if (shoppingListItemModel == null)
            {
                return NotFound();
            }
            ViewData["ShoppingListId"] = new SelectList(_context.ShoppingLists, "id", "id", shoppingListItemModel.ShoppingListId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", shoppingListItemModel.UserId);
            return View(shoppingListItemModel);
        }

        // POST: ShoppingListItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Price,Amount,ShoppingListId,UserId")] ShoppingListItemModel shoppingListItemModel)
        {
            if (id != shoppingListItemModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingListItemModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingListItemModelExists(shoppingListItemModel.id))
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
            ViewData["ShoppingListId"] = new SelectList(_context.ShoppingLists, "id", "id", shoppingListItemModel.ShoppingListId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", shoppingListItemModel.UserId);
            return View(shoppingListItemModel);
        }

        // GET: ShoppingListItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingListItemModel = await _context.ShoppingListItemModel
                .Include(s => s.ShoppingList)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.id == id);
            if (shoppingListItemModel == null)
            {
                return NotFound();
            }

            return View(shoppingListItemModel);
        }

        // POST: ShoppingListItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingListItemModel = await _context.ShoppingListItemModel.FindAsync(id);
            _context.ShoppingListItemModel.Remove(shoppingListItemModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingListItemModelExists(int id)
        {
            return _context.ShoppingListItemModel.Any(e => e.id == id);
        }
    }
}
