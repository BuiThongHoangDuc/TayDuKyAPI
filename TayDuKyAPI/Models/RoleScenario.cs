using System;
using System.Collections.Generic;

namespace TayDuKyAPI.Models
{
    public partial class RoleScenario
    {
        public RoleScenario()
        {
            ActorRoles = new HashSet<ActorRole>();
        }

        public int RoleScenarioId { get; set; }
        public string RoleScenarioName { get; set; }

        public virtual ICollection<ActorRole> ActorRoles { get; set; }
    }
}
