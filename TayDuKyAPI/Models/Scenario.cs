using System;
using System.Collections.Generic;

namespace TayDuKyAPI.Models
{
    public partial class Scenario
    {
        public Scenario()
        {
            ActorRoles = new HashSet<ActorRole>();
            EquipmentInScenarios = new HashSet<EquipmentInScenario>();
        }

        public int ScenarioId { get; set; }
        public string ScenarioName { get; set; }
        public string ScenarioDes { get; set; }
        public string ScenarioLocation { get; set; }
        public DateTime? ScenarioTimeFrom { get; set; }
        public DateTime? ScenarioTimeTo { get; set; }
        public int? ScenarioCastAmout { get; set; }
        public int? ScenarioStatus { get; set; }
        public string ScenarioImage { get; set; }

        public virtual ICollection<ActorRole> ActorRoles { get; set; }
        public virtual ICollection<EquipmentInScenario> EquipmentInScenarios { get; set; }
    }
}
