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
    public class ActorRolesController : ControllerBase
    {
        private readonly IActorIInScenarioSV _actorInScSV;

        public ActorRolesController(IActorIInScenarioSV actorInScSV)
        {
            _actorInScSV = actorInScSV;
        }

        // DELETE: api/ActorRoles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActorRole>> DeleteActorRole(int id)
        {
            try
            {
                var isDelete = await _actorInScSV.DeleteActorInScenarioSV(id);
                if (isDelete) return NoContent();
                else return NotFound();
            }
            catch (Exception) { return BadRequest(); }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActorInScenarioDetail>> GetActorInScenarioByID(int id)
        {
            var ar = await _actorInScSV.GetActorInScenarioByIDSV(id).FirstOrDefaultAsync();

            if (ar == null)
            {
                return NotFound();
            }

            return Ok(ar);
        }

        //PUT: api/Equipments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAr(int id, ActorInScenarioAddVM arEditModel)
        {
            if (id != arEditModel.ActorRoleId)
            {
                return BadRequest();
            }

            try
            {
                int idUpdate = await _actorInScSV.UpdateARSV(id, arEditModel);
                if (idUpdate == -1) return NotFound();
                else return Ok(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
