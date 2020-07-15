using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TayDuKyAPI.Enums;
using TayDuKyAPI.Models;
using TayDuKyAPI.ViewModel;

namespace TayDuKyAPI.Repository
{
    public class ScenarioRepository : IScenarioRepository
    {
        private readonly PRM391Context _context;

        public ScenarioRepository(PRM391Context context)
        {
            _context = context;
        }

        public async Task AddScenario(ScenarioInfoVM scenario)
        {
            Scenario scenarioModel = new Scenario();
            scenarioModel.ScenarioName = scenario.ScenarioName;
            scenarioModel.ScenarioDes = scenario.ScenarioDes;
            scenarioModel.ScenarioImage = scenario.ScenarioImage;
            scenarioModel.ScenarioCastAmout = scenario.ScenarioCastAmout;
            scenarioModel.ScenarioLocation = scenario.ScenarioLocation;
            scenarioModel.ScenarioTimeFrom = scenario.ScenarioTimeFrom;
            scenarioModel.ScenarioTimeTo = scenario.ScenarioTimeTo;
            scenarioModel.ScenarioStatus = Status.INPROCESS;
            scenarioModel.ScenarioIsDelete = IsDelete.ACTIVE;
            scenarioModel.ScenarioScript = scenario.ScenarioScript;

            _context.Scenarios.Add(scenarioModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) {
                throw;
            }
            
        }

        public async Task<bool> DeleteScenario(int id)
        {
            var scenario = await _context.Scenarios.FindAsync(id);
            if (scenario == null)
            {
                return false;
            }
            try
            {
                scenario.ScenarioIsDelete = IsDelete.ISDELETED;
                _context.Entry(scenario).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public IQueryable<ScenarioBasicInfoVM> GetListScenario()
        {
            var listScenario = _context.Scenarios
                                        .Where(sc => sc.ScenarioIsDelete.Equals(IsDelete.ACTIVE))
                                        .Select(sc => new ScenarioBasicInfoVM
                                        {
                                            ScenarioId = sc.ScenarioId,
                                            ScenarioName = sc.ScenarioName,
                                            ScenarioDes = sc.ScenarioDes,
                                            ScenarioImage = sc.ScenarioImage,
                                            ScenarioLocation = sc.ScenarioLocation,
                                            ScenarioStatus = sc.ScenarioStatus,
                                            ScenarioTimeFrom = sc.ScenarioTimeFrom,
                                            ScenarioTimeTo = sc.ScenarioTimeTo,
                                        });
            return listScenario;
        }
        public IQueryable<ScenarioEditInfoVM> GetScenario(int id)
        {
            var scenario = _context.Scenarios
                                    .Where(sc => sc.ScenarioId == id && sc.ScenarioIsDelete == IsDelete.ACTIVE)
                                    .Select(sc => new ScenarioEditInfoVM
                                    {
                                        ScenarioId = sc.ScenarioId,
                                        ScenarioCastAmout = sc.ScenarioCastAmout,
                                        ScenarioDes = sc.ScenarioDes,
                                        ScenarioImage = sc.ScenarioImage,
                                        ScenarioLocation = sc.ScenarioLocation,
                                        ScenarioName = sc.ScenarioName,
                                        ScenarioStatus= sc.ScenarioStatus,
                                        ScenarioTimeFrom = sc.ScenarioTimeFrom,
                                        ScenarioTimeTo = sc.ScenarioTimeTo,
                                        ScenarioScript = sc.ScenarioScript,
                                    });
            return scenario;
        }

        public IQueryable<ScenarioBasicInfoVM> SearchByNameScenario(string sName)
        {
            var listScenario = _context.Scenarios
                                        .Where(sc => sc.ScenarioName.Contains(sName) && sc.ScenarioIsDelete.Equals(IsDelete.ACTIVE))
                                        .Select(sc => new ScenarioBasicInfoVM
                                        {
                                            ScenarioId = sc.ScenarioId,
                                            ScenarioName = sc.ScenarioName,
                                            ScenarioDes = sc.ScenarioDes,
                                            ScenarioImage = sc.ScenarioImage,
                                            ScenarioLocation = sc.ScenarioLocation,
                                        });
            return listScenario;
        }

        public async Task<int> UpdateScenario(int id, ScenarioEditInfoVM scenario)
        {
            Scenario scenarioModel = await _context.Scenarios.FindAsync(id);
            if (scenarioModel == null) return -1;
            scenarioModel.ScenarioName = scenario.ScenarioName;
            scenarioModel.ScenarioDes = scenario.ScenarioDes;
            scenarioModel.ScenarioLocation = scenario.ScenarioLocation;
            scenarioModel.ScenarioTimeFrom = scenario.ScenarioTimeFrom;
            scenarioModel.ScenarioTimeTo = scenario.ScenarioTimeTo;
            scenarioModel.ScenarioCastAmout = scenario.ScenarioCastAmout;
            scenarioModel.ScenarioStatus = scenario.ScenarioStatus;
            scenarioModel.ScenarioImage = scenario.ScenarioImage;
            scenarioModel.ScenarioScript = scenario.ScenarioScript;

            _context.Entry(scenarioModel).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
                return scenarioModel.ScenarioId;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScenarioExists(id))
                {
                    return -1;
                }
                else throw;
            }

        }


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

        private bool ScenarioExists(int id)
        {
            return _context.Scenarios.Any(e => e.ScenarioId == id);
        }

    }

    public interface IScenarioRepository
    {
        IQueryable<ScenarioBasicInfoVM> GetListScenario();
        IQueryable<ScenarioBasicInfoVM> SearchByNameScenario(string sName);
        Task AddScenario(ScenarioInfoVM scenario);
        Task<bool> DeleteScenario(int id);
        IQueryable<ScenarioEditInfoVM> GetScenario(int id);
        Task<int> UpdateScenario(int id, ScenarioEditInfoVM scenario);
    }
}
