using System;
using System.Collections.Generic;

namespace TayDuKyAPI.Models
{
    public partial class Equipment
    {
        public Equipment()
        {
            EquipmentInScenarios = new HashSet<EquipmentInScenario>();
        }

        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentDes { get; set; }
        public string EquipmentImage { get; set; }
        public int EquipmentQuantity { get; set; }
        public int? EquipmentStatus { get; set; }
        public int? EquipmentIsDelete { get; set; }

        public virtual ICollection<EquipmentInScenario> EquipmentInScenarios { get; set; }
    }
}
