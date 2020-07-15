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
    public class ScenariosController : ControllerBase
    {
        private readonly IScenarioService _scenario;
        private readonly IEquipmentInScenarioSV _eis;

        public ScenariosController(IScenarioService scenario, IEquipmentInScenarioSV eis)
        {
            _scenario = scenario;
            _eis = eis;
        }

        // GET: api/Scenarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScenarioBasicInfoVM>>> SearchByNameScenarios(string ScenarioName)
        {
            if (ScenarioName == null) return NotFound();
            var listSenario = await _scenario.SearchByNameScenarioVM(ScenarioName).ToListAsync();
            if (listSenario.Count == 0) return NotFound();
            else return Ok(listSenario);
        }

        // GET: api/Scenarios
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<ScenarioBasicInfoVM>>> GetScenarios()
        {
            var listSenario = await _scenario.GetListScenarioSV().ToListAsync();
            if (listSenario.Count == 0) return NotFound();
            else return Ok(listSenario);
        }
        //POST: api/Scenarios
        [HttpPost]
        public async Task<ActionResult> AddScenario(ScenarioInfoVM scenario)
        {
            try
            {
                await _scenario.AddScenarioVM(scenario);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NoContent();
        }


        //POST: api/Scenarios
        [HttpPost("{id}/ActorRole")]
        public async Task<ActionResult> AddActorInScenario(int id, ActorInScenarioAddVM addModel)
        {
            if (id != addModel.ScenarioId)
            {
                return BadRequest();
            }

            try
            {
                var check = await _scenario.AddActorInScenarioSV(addModel);
                if (check == true) return NoContent();
                else return Conflict();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //DELETE: api/Scenarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteScenario(int id)
        {
            try
            {
                var isDelete = await _scenario.DeleteScenarioSV(id);
                if (isDelete) return NoContent();
                else return NotFound();
            }
            catch (Exception) { return BadRequest(); }
        }


        //GET: api/Scenarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScenarioEditInfoVM>> GetScenario(int id)
        {
            var scenario = await _scenario.GetScenarioSV(id).FirstOrDefaultAsync();

            if (scenario == null)
            {
                return NotFound();
            }

            return Ok(scenario);
        }

        //GET: api/Scenarios/5/ActorRole
        [HttpGet("{id}/ActorRole")]
        public async Task<ActionResult<ActorInScenarioListVM>> GetListActorRole(int id)
        {
            var listActorInSC = await _scenario.GetListActorInScenarioSV(id).ToListAsync();
            if (listActorInSC.Count == 0) return NotFound();
            else return Ok(listActorInSC);
        }


        //GET: api/Scenarios/5/ActorRole
        [HttpGet("{id}/EquipmentInScenario")]
        public async Task<ActionResult<ActorInScenarioListVM>> GetListEquipmentInSC(int id)
        {
            var listEquipmentInSC = await _eis.GetListEquipmentInScenarioSV(id).ToListAsync();
            if (listEquipmentInSC.Count == 0) return NotFound();
            else return Ok(listEquipmentInSC);
        }

        //PUT: api/Scenarios/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutScenario(int id, ScenarioEditInfoVM scenario)
        {
            if (id != scenario.ScenarioId)
            {
                return BadRequest();
            }

            try
            {
                int idUpdate = await _scenario.UpdateScenarioVM(id, scenario);
                if (idUpdate == -1) return NotFound();
                else return Ok(id);
            }
            catch (Exception) {
                return BadRequest();
            }

        }

        [HttpPost("{id}/EquipmentInScenario")]
        public async Task<ActionResult> AddEquipmentISC(int id, EquipInScenarioAddModel addModel)
        {

            if (id != addModel.ScenarioId)
            {
                return BadRequest();
            }

            try
            {
                var check = await _eis.AddEquipmentInScenarioSV(addModel);
                if (check == true) return NoContent();
                else return Conflict();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
