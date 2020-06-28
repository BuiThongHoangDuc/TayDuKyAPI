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
                                        });
            return listScenario;
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
    }

    public interface IScenarioRepository
    {
        IQueryable<ScenarioBasicInfoVM> GetListScenario(); 
        IQueryable<ScenarioBasicInfoVM> SearchByNameScenario(string sName);
    }
}
