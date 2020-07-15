using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TayDuKyAPI.Enums;
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
            actorInRole.IsDelete = IsDelete.ACTIVE;


            _context.ActorRoles.Add(actorInRole);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                Debug.WriteLine(e.InnerException.Message);
                throw;
            }
        }

        public async Task<bool> DeleteActorInScenario(int actorInScenarioID)
        {
            var actorInScenario = await _context.ActorRoles.FindAsync(actorInScenarioID);
            if (actorInScenario == null)
            {
                return false;
            }
            try
            {
                actorInScenario.IsDelete = IsDelete.ISDELETED;
                _context.Entry(actorInScenario).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<bool> FindActorInScenario(ActorInScenarioAddVM addModel)
        {
            var actorInSC = await _context.ActorRoles.Where(ar => ar.ActorInScenario == addModel.ActorInScenario && ar.RoleScenarioId == addModel.RoleScenarioId && ar.ScenarioId == addModel.ScenarioId && ar.IsDelete == IsDelete.ACTIVE).FirstOrDefaultAsync();
            if (actorInSC == null) return false;
            else return true;
        }

        public IQueryable<ActorInScenarioDetail> GetActorInScenarioByID(int id)
        {
            var ActorRole = _context.ActorRoles
                                   .Where(ar => ar.ActorRoleId == id && ar.IsDelete == IsDelete.ACTIVE && ar.ActorInScenarioNavigation.UserIsDelete == IsDelete.ACTIVE)
                                   .Select(ar => new ActorInScenarioDetail
                                   {
                                       ActorRoleId = ar.ActorRoleId,
                                       ActorInScenario = ar.ActorInScenario,
                                       ActorRoleDescription = ar.ActorRoleDescription,
                                       DateUpdate = ar.DateUpdate,
                                       RoleScenarioId = ar.RoleScenarioId,
                                       Admin = ar.AdminNavigation.UserEmail,
                                   });
            return ActorRole;
        }

        public IQueryable<ActorInScenarioListVM> GetListActorInScenario(int scenarioID)
        {
            var actorInSC = _context.ActorRoles
                                            .Where(ar => ar.ScenarioId == scenarioID && ar.IsDelete == IsDelete.ACTIVE)
                                            .Select(ar => new ActorInScenarioListVM
                                            {
                                                RoleScenarioId = ar.RoleScenario.RoleScenarioName,
                                                ActorInScenario = ar.ActorInScenarioNavigation.UserName,
                                                ActorRoleId = ar.ActorRoleId,
                                                Admin = ar.AdminNavigation.UserName,
                                                DateUpdate = ar.DateUpdate,
                                                ActorEmail = ar.ActorInScenarioNavigation.UserEmail,
                                            });
            return actorInSC;
        }

        public async Task<int> UpdateAR(int id, ActorInScenarioAddVM arEditModel)
        {
            ActorRole ar = await _context.ActorRoles.FindAsync(id);
            if (ar == null) return -1;
            ar.ActorInScenario = arEditModel.ActorInScenario;
            ar.RoleScenarioId = arEditModel.RoleScenarioId;
            ar.ActorRoleDescription = arEditModel.ActorRoleDescription;
            ar.DateUpdate = arEditModel.DateUpdate;
            ar.Admin = arEditModel.Admin;

            _context.Entry(ar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return ar.ActorRoleId;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArExists(id))
                {
                    return -1;
                }
                else throw;
            }
        }
        private bool ArExists(int id)
        {
            return _context.ActorRoles.Any(e => e.ActorRoleId == id);
        }

        public IQueryable<ScenarioBasicInfoVM> GetListScenarioIsDone(int id)
        {
            var scenarioList = _context.ActorRoles.Where(ar => ar.ActorInScenario == id && ar.Scenario.ScenarioStatus == Status.DONE).Select(ar => new ScenarioBasicInfoVM
            {
                ScenarioId = ar.ScenarioId.Value,
                ScenarioImage = ar.Scenario.ScenarioImage,
                ScenarioLocation = ar.Scenario.ScenarioLocation,
                ScenarioName = ar.Scenario.ScenarioName,
                ScenarioStatus = ar.Scenario.ScenarioStatus,
                ScenarioTimeFrom = ar.Scenario.ScenarioTimeFrom,
                ScenarioTimeTo = ar.Scenario.ScenarioTimeTo,
                ScenarioScript = ar.Scenario.ScenarioScript,
            }).Distinct();
            return scenarioList;
        }

        public IQueryable<ScenarioBasicInfoVM> GetListScenarioIsStillAvaliable(int id)
        {
            var scenarioList = _context.ActorRoles.Where(ar => ar.ActorInScenario == id && ar.Scenario.ScenarioStatus == Status.INPROCESS).Select(ar => new ScenarioBasicInfoVM
            {
                ScenarioId = ar.ScenarioId.Value,
                ScenarioImage = ar.Scenario.ScenarioImage,
                ScenarioLocation = ar.Scenario.ScenarioLocation,
                ScenarioName = ar.Scenario.ScenarioName,
                ScenarioStatus = ar.Scenario.ScenarioStatus,
                ScenarioTimeFrom = ar.Scenario.ScenarioTimeFrom,
                ScenarioTimeTo = ar.Scenario.ScenarioTimeTo,
                ScenarioScript = ar.Scenario.ScenarioScript,
            }).Distinct();
            return scenarioList;
        }

        public IQueryable<ActorInScenarioListVM> GetListActorInScenarioByID(int actorID, int scenarioID)
        {
            var actorInSC = _context.ActorRoles
                                            .Where(ar => ar.ScenarioId == scenarioID && ar.IsDelete == IsDelete.ACTIVE && ar.ActorInScenario == actorID)
                                            .Select(ar => new ActorInScenarioListVM
                                            {
                                                RoleScenarioId = ar.RoleScenario.RoleScenarioName,
                                                ActorInScenario = ar.ActorInScenarioNavigation.UserName,
                                                ActorRoleId = ar.ActorRoleId,
                                                Admin = ar.AdminNavigation.UserName,
                                                DateUpdate = ar.DateUpdate,
                                                ActorEmail = ar.ActorInScenarioNavigation.UserEmail,
                                            });
            return actorInSC;
        }
    }
    public interface IActorInScenarioRepo {
        Task AddActorInScenario(ActorInScenarioAddVM addModel);
        IQueryable<ActorInScenarioListVM> GetListActorInScenario(int scenarioID);
        IQueryable<ActorInScenarioListVM> GetListActorInScenarioByID(int actorID,int scenarioID);
        Task<bool> FindActorInScenario(ActorInScenarioAddVM addModel);
        Task<bool> DeleteActorInScenario(int actorInScenarioID);
        IQueryable<ActorInScenarioDetail> GetActorInScenarioByID(int id);
        Task<int> UpdateAR(int id, ActorInScenarioAddVM arEditModel);
        IQueryable<ScenarioBasicInfoVM> GetListScenarioIsStillAvaliable(int id);
        IQueryable<ScenarioBasicInfoVM> GetListScenarioIsDone(int id);

    }
}
