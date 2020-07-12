using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TayDuKyAPI.Repository;
using TayDuKyAPI.ViewModel;

namespace TayDuKyAPI.Service
{
    public class ScenarioService : IScenarioService
    {
        private readonly IScenarioRepository _scenario; 
        private readonly IActorInScenarioRepo _ar; 
        
        public ScenarioService(IScenarioRepository scenario, IActorInScenarioRepo ar)
        {
            _scenario = scenario;
            _ar = ar;
        }

        public Task AddActorInScenarioSV(ActorInScenarioAddVM addModel)
        {
            return _ar.AddActorInScenario(addModel);
        }

        public async Task AddScenarioVM(ScenarioInfoVM scenario)
        {
            await _scenario.AddScenario(scenario);
        }

        public async Task<bool> DeleteScenarioSV(int id)
        {
            try
            {
                bool check = await _scenario.DeleteScenario(id);
                if (check == true) return true;
                else return false;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public IQueryable<ActorInScenarioListVM> GetListActorInScenarioSV(int scenarioID)
        {
            return _ar.GetListActorInScenario(scenarioID);
        }

        public IQueryable<ScenarioBasicInfoVM> GetListScenarioSV()
        {
            return _scenario.GetListScenario();
        }

        public IQueryable<ScenarioEditInfoVM> GetScenarioSV(int id)
        {
            return _scenario.GetScenario(id);
        }

        public IQueryable<ScenarioBasicInfoVM> SearchByNameScenarioVM(string sName)
        {
            sName = sName.Trim();
            return _scenario.SearchByNameScenario(sName);
        }

        public Task<int> UpdateScenarioVM(int id, ScenarioEditInfoVM scenario)
        {
            return _scenario.UpdateScenario(id, scenario);
        }
    }
    public interface IScenarioService
    {
        IQueryable<ScenarioBasicInfoVM> GetListScenarioSV();
        IQueryable<ScenarioBasicInfoVM> SearchByNameScenarioVM(string sName);
        Task AddScenarioVM(ScenarioInfoVM scenario);
        Task<bool> DeleteScenarioSV(int id);
        IQueryable<ScenarioEditInfoVM> GetScenarioSV(int id);
        Task<int> UpdateScenarioVM(int id, ScenarioEditInfoVM scenario);
        IQueryable<ActorInScenarioListVM> GetListActorInScenarioSV(int scenarioID);
        Task AddActorInScenarioSV(ActorInScenarioAddVM addModel);


    }
}
