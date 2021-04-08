using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using tomiris.Models;
using tomiris.ViewModels;
using System.Text;
using System.Web;

//TODO сделать CRUD для публикаций

namespace tomiris.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly TomirisContext db;

        public BlogPostController(TomirisContext context)
        {
            db = context;
            if (!db.BlogPosts.Any())
            {
                db.BlogPosts.Add(new BlogPostModel { Name = "Лямбда-оператор", Text = "В лямбда-выражениях лямбда-оператор => используется для отделения входных параметров с левой стороны от тела лямбда-выражения с правой стороны." });
                db.BlogPosts.Add(new BlogPostModel { Name = "Определения тела выражения", Text = "Тип возвращаемого значения expression должен быть неявно преобразуемым в тип возвращаемого значения элемента. Если для элемента возвращается значение типа void или элемент является конструктором, методом завершения или методом доступа свойства или индексатора set, значение expression должно быть выражением оператора. " });
                db.SaveChanges();
            }
        }

        // 
        // GET: Index

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.postsList = db.BlogPosts.ToList();
            return View();
        }

        //
        //CRUD
        //

        [HttpGet]
        public async Task<ActionResult<BlogPostModel>> Get(int id)
        {
            BlogPostModel post = await db.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return new ObjectResult(post);
        }

        [HttpPost]
        public async Task<ActionResult<BlogPostModel>> Post(BlogPostModel post)
        {
            if (post == null)
            {
                return BadRequest();
            }
            db.BlogPosts.Add(post);
            await db.SaveChangesAsync();
            return new ObjectResult(post);
        }

        [HttpPut]
        public async Task<ActionResult<BlogPostModel>> Put(BlogPostModel post)
        {
            if (post == null)
            {
                return BadRequest();
            }
            if (!db.BlogPosts.Any(x => x.Id == post.Id))
            {
                return NotFound();
            }
            db.Update(post);
            await db.SaveChangesAsync();
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BlogPostModel>> Delete(int id)
        {
            BlogPostModel post = await db.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            db.BlogPosts.Remove(post);
            await db.SaveChangesAsync();
            return Ok(post);
        }

        //
        //CRUD
        //

        [HttpGet]
        [Route("/BlogPost/Read/{id}")]
        public async Task<ActionResult> Read(int id)
        {
            BlogPostModel post = await db.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            Debug.WriteLine($"Data: {post.Name} {post.Text}");
            ViewData["postName"] = post.Name;
            ViewData["postText"] = post.Text;
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            BlogPostModel post = await db.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            ViewData["postId"] = post.Id;
            ViewData["postName"] = post.Name;
            ViewData["postText"] = post.Text;
            return View(post);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var postToUpdate = await db.BlogPosts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (postToUpdate == null)
            {
                return NotFound();
            }
            if(await TryUpdateModelAsync<BlogPostModel>(postToUpdate,
                    "",
                    c => c.Id, c => c.Name, c => c.Text))
    {
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(postToUpdate);
        }
    }
}