using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TayDuKyAPI.Models;
using TayDuKyAPI.ViewModel;

namespace TayDuKyAPI.Repository
{
    public class RoleScenarioRepo : IRoleScenarioRepo
    {
        private readonly PRM391Context _context;

        public RoleScenarioRepo(PRM391Context context)
        {
            _context = context;
        }

        public IQueryable<RoleActorVM> GetListRole()
        {
            var listRole = _context.RoleScenarios
                                            .Select(role => new RoleActorVM
                                            {
                                                RoleScenarioId = role.RoleScenarioId,
                                                RoleScenarioName = role.RoleScenarioName,
                                            });
            return listRole;
        }
    }
    public interface IRoleScenarioRepo
    {
        IQueryable<RoleActorVM> GetListRole();

    }
}
