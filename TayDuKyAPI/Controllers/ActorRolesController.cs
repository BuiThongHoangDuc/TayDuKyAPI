using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TayDuKyAPI.Enums;
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
        private readonly PRM391Context _context;


        public ActorRolesController(IActorIInScenarioSV actorInScSV, PRM391Context context)
        {
            _actorInScSV = actorInScSV;
            _context = context;
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

        // GET: api/Scenarios
        [HttpGet("{id}/ListInprocess")]
        public async Task<ActionResult<IEnumerable<ScenarioBasicInfoVM>>> GetScenariosInProcess(int id)
        {
            var listSenario = await _actorInScSV.GetListScenarioIsStillAvaliableSV(id).ToListAsync();
            if (listSenario.Count == 0) return NotFound();
            else return Ok(listSenario);
        }
        // GET: api/Scenarios
        [HttpGet("{id}/ListDone")]
        public async Task<ActionResult<IEnumerable<ScenarioBasicInfoVM>>> GetScenariosDone(int id)
        {
            var listSenario = await _actorInScSV.GetListScenarioIsDoneSV(id).ToListAsync();
            if (listSenario.Count == 0) return NotFound();
            else return Ok(listSenario);
        }

        // GET: api/Scenarios
        [HttpGet("{id}/{ScenarioID}")]
        public async Task<ActionResult> GetScenariosDone1(int id, int ScenarioID)
        {
            var listSenario = await _actorInScSV.GetListActorInScenarioByIDSV(id, ScenarioID).ToListAsync();
            if (listSenario.Count == 0) return NotFound();
            else return Ok(listSenario);
        }
    }
}
