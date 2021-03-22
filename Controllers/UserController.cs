using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using tomiris.Models;
 
namespace tomiris.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        readonly TomirisContext db;
        public UsersController(TomirisContext context)
        {
            db = context;
            if (!db.Users.Any())
            {
                db.Users.Add(new UserModel { Name = "Tom", Age = 26 });
                db.Users.Add(new UserModel { Name = "Alice", Age = 31 });
                db.SaveChanges();
            }
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            return await db.Users.ToListAsync();
        }
 
        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            UserModel user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }
 
        // POST api/users
        [HttpPost]
        public async Task<ActionResult<UserModel>> Post(UserModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
 
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
 
        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<UserModel>> Put(UserModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.Users.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }
 
            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
 
        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> Delete(int id)
        {
            UserModel user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}