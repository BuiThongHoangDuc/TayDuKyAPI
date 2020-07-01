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
        public ScenarioService(IScenarioRepository scenario)
        {
            _scenario = scenario;
        }

        public async Task AddScenarioVM(ScenarioInfoVM scenario)
        {
            await _scenario.AddScenario(scenario);
        }

        public IQueryable<ScenarioBasicInfoVM> GetListScenarioSV()
        {
            return _scenario.GetListScenario();
        }

        public IQueryable<ScenarioBasicInfoVM> SearchByNameScenarioVM(string sName)
        {
            sName = sName.Trim();
            return _scenario.SearchByNameScenario(sName);
        }
    }
    public interface IScenarioService
    {
        IQueryable<ScenarioBasicInfoVM> GetListScenarioSV();
        IQueryable<ScenarioBasicInfoVM> SearchByNameScenarioVM(string sName);
        Task AddScenarioVM(ScenarioInfoVM scenario);

    }
}
