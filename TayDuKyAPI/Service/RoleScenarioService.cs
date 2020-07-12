using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TayDuKyAPI.Repository;
using TayDuKyAPI.ViewModel;

namespace TayDuKyAPI.Service
{
    public class RoleScenarioService : IRoleScenarioService
    {
        private readonly IRoleScenarioRepo _roleRepo;

        public RoleScenarioService(IRoleScenarioRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public IQueryable<RoleActorVM> GetListRoleSV()
        {
            return _roleRepo.GetListRole();
        }
    }
    public interface IRoleScenarioService {
        IQueryable<RoleActorVM> GetListRoleSV();

    }
}
