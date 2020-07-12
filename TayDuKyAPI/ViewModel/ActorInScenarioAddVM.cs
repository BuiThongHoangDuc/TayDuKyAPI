using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TayDuKyAPI.ViewModel
{
    public class ActorInScenarioAddVM
    {
        public int ActorRoleId { get; set; }
        public int? ActorInScenario { get; set; }
        public int? RoleScenarioId { get; set; }
        public int? ScenarioId { get; set; }
        public string ActorRoleDescription { get; set; }
        public DateTime? DateUpdate { get; set; }
        public int? Admin { get; set; }
    }
}
