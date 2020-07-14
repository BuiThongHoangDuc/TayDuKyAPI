using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TayDuKyAPI.ViewModel
{
    public class EquipmentInScenarioDetailVM
    {
        public int EquipInScenario { get; set; }
        public String ScenarioName { get; set; }
        public String EquipmentName { get; set; }
        public int? EquipmentQuantity { get; set; }
        public DateTime? CreateByDate { get; set; }
        public DateTime? UpdateByDate { get; set; }
        public string PersonUpdate { get; set; }
    }
}
