using System;
using System.Collections.Generic;

namespace TayDuKyAPI.Models
{
    public partial class ActorRole
    {
        public int ActorRoleId { get; set; }
        public int? ActorInScenario { get; set; }
        public int? RoleScenarioId { get; set; }
        public int? ScenarioId { get; set; }
        public string ActorRoleDescription { get; set; }
        public DateTime? DateUpdate { get; set; }
        public int? Admin { get; set; }
        public int? IsDelete { get; set; }

        public virtual User ActorInScenarioNavigation { get; set; }
        public virtual User AdminNavigation { get; set; }
        public virtual RoleScenario RoleScenario { get; set; }
        public virtual Scenario Scenario { get; set; }
    }
}
