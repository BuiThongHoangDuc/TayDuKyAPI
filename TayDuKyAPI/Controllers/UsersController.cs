using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TayDuKyAPI.Models;
using TayDuKyAPI.Service;
using TayDuKyAPI.ViewModel;

namespace TayDuKyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorBasicInfoVM>>> SearchUsers(string usName)
        {
            if (usName == null) return NotFound();
            var list = await _userService.SearchActorVM(usName).ToListAsync();
            if (list.Count == 0) return NotFound();
            else return Ok(list);
        }

        // GET: api/Users
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<ActorBasicInfoVM>>> GetUsers()
        {
            var list = await _userService.GetListActorVM().ToListAsync();
            if (list.Count == 0) return NotFound();
            else return Ok(list);
        }

        //POST: api/Users
        [HttpPost]
        public async Task<ActionResult> AddActor(ActorInfoVM actor)
        {
            try
            {
                await _userService.AddActorSV(actor);
            }
            catch (Exception) {
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var isDelete = await _userService.DeleteActorSV(id);
                if (isDelete) return NoContent();
                else return NotFound();
            }
            catch (Exception) { return BadRequest(); }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorInfoVM>> GetUser(int id)
        {
            var user = await _userService.GetActorSV(id).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //// PUT: api/Users/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int id, User user)
        //{
        //    if (id != user.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Users
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        //}


        //private bool UserExists(int id)
        //{
        //    return _context.Users.Any(e => e.UserId == id);
        //}
    }
}
