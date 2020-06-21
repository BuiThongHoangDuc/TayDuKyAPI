using System;
using System.Collections.Generic;

namespace TayDuKyAPI.Models
{
    public partial class EquipmentInScenario
    {
        public int EquipInScenario { get; set; }
        public int? ScenarioId { get; set; }
        public int? EquipmentId { get; set; }
        public int? EquipmentQuantity { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual Scenario Scenario { get; set; }
    }
}
