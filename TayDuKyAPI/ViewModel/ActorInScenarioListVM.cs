using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TayDuKyAPI.ViewModel
{
    public class ActorInScenarioListVM
    {
        public int ActorRoleId { get; set; }
        public String ActorInScenario { get; set; }
        public String RoleScenarioId { get; set; }
        public DateTime? DateUpdate { get; set; }
        public String Admin { get; set; }
    }
}
