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
    public class LoginController : ControllerBase
    {
        private readonly IUserService _user;

        public LoginController(IUserService user)
        {
            _user = user;
        }

        // GET: api/Login
        [HttpGet]
        public async Task<ActionResult<LoginViewModel>> GetUsers(string email, string password)
        {
            var info = await _user.CheckLoginSV(email, password).FirstOrDefaultAsync();
            if (info == null) return NotFound();
            else return Ok(info);
            //return await _context.Users.ToListAsync();
        }
    }
}
