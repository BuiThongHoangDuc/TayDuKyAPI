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
    public class RoleScenariosController : ControllerBase
    {
        private readonly IRoleScenarioService _roleSV;

        public RoleScenariosController(IRoleScenarioService roleSV)
        {
            _roleSV = roleSV;
        }



        // GET: api/RoleScenarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleActorVM>>> GetRoleScenarios()
        {
            var list = await _roleSV.GetListRoleSV().ToListAsync();
            if (list.Count == 0) return NotFound();
            else return Ok(list);
        }
    }
}
