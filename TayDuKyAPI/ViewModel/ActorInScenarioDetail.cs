using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TayDuKyAPI.ViewModel
{
    public class ActorInScenarioDetail
    {
        public int ActorRoleId { get; set; }
        public int? ActorInScenario { get; set; }
        public int? RoleScenarioId { get; set; }
        public string ActorRoleDescription { get; set; }
        public DateTime? DateUpdate { get; set; }
        public String Admin { get; set; }
    }
}
