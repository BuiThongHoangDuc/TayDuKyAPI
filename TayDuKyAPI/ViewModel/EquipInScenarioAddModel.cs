using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TayDuKyAPI.ViewModel
{
    public class EquipInScenarioAddModel
    {
        public int EquipInScenario { get; set; }
        public int ScenarioId { get; set; }
        public int EquipmentId { get; set; }
        public int EquipmentQuantity { get; set; }
        public DateTime CreateByDate { get; set; }
        public DateTime UpdateByDate { get; set; }
        public string PersonUpdate { get; set; }
    }
}
