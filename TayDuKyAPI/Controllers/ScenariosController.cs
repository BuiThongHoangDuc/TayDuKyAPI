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

        public ScenariosController(IScenarioService scenario)
        {
            _scenario = scenario;
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
        

       // GET: api/Scenarios/5
       // [HttpGet("{id}")]
       // public async Task<ActionResult<Scenario>> GetScenario(int id)
       // {
       //     var scenario = await _context.Scenarios.FindAsync(id);

       //     if (scenario == null)
       //     {
       //         return NotFound();
       //     }

       //     return scenario;
       // }

       // PUT: api/Scenarios/5
       // [HttpPut("{id}")]
       // public async Task<IActionResult> PutScenario(int id, Scenario scenario)
       // {
       //     if (id != scenario.ScenarioId)
       //     {
       //         return BadRequest();
       //     }

       //     _context.Entry(scenario).State = EntityState.Modified;

       //     try
       //     {
       //         await _context.SaveChangesAsync();
       //     }
       //     catch (DbUpdateConcurrencyException)
       //     {
       //         if (!ScenarioExists(id))
       //         {
       //             return NotFound();
       //         }
       //         else
       //         {
       //             throw;
       //         }
       //     }

       //     return NoContent();
       // }

       // POST: api/Scenarios
       //[HttpPost]
       // public async Task<ActionResult<Scenario>> PostScenario(Scenario scenario)
       // {
       //     _context.Scenarios.Add(scenario);
       //     await _context.SaveChangesAsync();

       //     return CreatedAtAction("GetScenario", new { id = scenario.ScenarioId }, scenario);
       // }

       // DELETE: api/Scenarios/5
       // [HttpDelete("{id}")]
       // public async Task<ActionResult<Scenario>> DeleteScenario(int id)
       // {
       //     var scenario = await _context.Scenarios.FindAsync(id);
       //     if (scenario == null)
       //     {
       //         return NotFound();
       //     }

       //     _context.Scenarios.Remove(scenario);
       //     await _context.SaveChangesAsync();

       //     return scenario;
       // }

       // private bool ScenarioExists(int id)
       // {
       //     return _context.Scenarios.Any(e => e.ScenarioId == id);
       // }
    }
}
