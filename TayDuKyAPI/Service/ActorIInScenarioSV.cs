using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TayDuKyAPI.Repository;
using TayDuKyAPI.ViewModel;

namespace TayDuKyAPI.Service
{
    public class ActorIInScenarioSV : IActorIInScenarioSV
    {
        private readonly IActorInScenarioRepo _ar;

        public ActorIInScenarioSV(IActorInScenarioRepo ar)
        {
            _ar = ar;
        }

        public Task<bool> DeleteActorInScenarioSV(int actorInScenarioID)
        {
            return _ar.DeleteActorInScenario(actorInScenarioID);
        }

        public IQueryable<ActorInScenarioDetail> GetActorInScenarioByIDSV(int id)
        {
            return _ar.GetActorInScenarioByID(id);
        }

        public Task<int> UpdateARSV(int id, ActorInScenarioAddVM arEditModel)
        {
            return _ar.UpdateAR(id, arEditModel);
        }


        public IQueryable<ScenarioBasicInfoVM> GetListScenarioIsDoneSV(int id)
        {
            return _ar.GetListScenarioIsDone(id);
        }

        public IQueryable<ScenarioBasicInfoVM> GetListScenarioIsStillAvaliableSV(int id)
        {
            return _ar.GetListScenarioIsStillAvaliable(id);
        }

        public IQueryable<ActorInScenarioListVM> GetListActorInScenarioByIDSV(int actorID, int scenarioID)
        {
            return _ar.GetListActorInScenarioByID(actorID, scenarioID);
        }
    }
    public interface IActorIInScenarioSV
    {
        Task<bool> DeleteActorInScenarioSV(int actorInScenarioID);
        IQueryable<ActorInScenarioDetail> GetActorInScenarioByIDSV(int id);
        Task<int> UpdateARSV(int id, ActorInScenarioAddVM arEditModel);
        IQueryable<ScenarioBasicInfoVM> GetListScenarioIsStillAvaliableSV(int id);
        IQueryable<ScenarioBasicInfoVM> GetListScenarioIsDoneSV(int id);
        IQueryable<ActorInScenarioListVM> GetListActorInScenarioByIDSV(int actorID, int scenarioID);


    }
}
