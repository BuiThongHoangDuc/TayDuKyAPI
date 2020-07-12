using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TayDuKyAPI.Models;
using TayDuKyAPI.ViewModel;

namespace TayDuKyAPI.Repository
{
    public class ActorInScenarioRepo : IActorInScenarioRepo
    {
        private readonly PRM391Context _context;

        public ActorInScenarioRepo(PRM391Context context)
        {
            _context = context;
        }

        public async Task AddActorInScenario(ActorInScenarioAddVM addModel)
        {
            ActorRole actorInRole = new ActorRole();
            actorInRole.ActorInScenario = addModel.ActorInScenario;
            actorInRole.RoleScenarioId = addModel.RoleScenarioId;
            actorInRole.ScenarioId = addModel.ScenarioId;
            actorInRole.ActorRoleDescription = addModel.ActorRoleDescription;
            actorInRole.DateUpdate = addModel.DateUpdate;
            actorInRole.Admin = addModel.Admin;


            _context.ActorRoles.Add(actorInRole);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public IQueryable<ActorInScenarioListVM> GetListActorInScenario(int scenarioID)
        {
            var actorInSC = _context.ActorRoles
                                            .Where(ar => ar.ScenarioId == scenarioID)
                                            .Select(ar => new ActorInScenarioListVM
                                            {
                                                RoleScenarioId = ar.RoleScenario.RoleScenarioName,
                                                ActorInScenario = ar.ActorInScenarioNavigation.UserName,
                                                ActorRoleId = ar.ActorRoleId,
                                                Admin = ar.AdminNavigation.UserName,
                                                DateUpdate = ar.DateUpdate,
                                            });
            return actorInSC;
        }
    }
    public interface IActorInScenarioRepo {
        Task AddActorInScenario(ActorInScenarioAddVM addModel);
        IQueryable<ActorInScenarioListVM> GetListActorInScenario(int scenarioID);
    }
}
