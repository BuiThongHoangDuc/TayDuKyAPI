using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TayDuKyAPI.Repository;
using TayDuKyAPI.ViewModel;

namespace TayDuKyAPI.Service
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipment;
        public EquipmentService(IEquipmentRepository equipment)
        {
            _equipment = equipment;
        }

        public async Task AddEquipmentSV(EquipmentInfoVM equipment)
        {
            await _equipment.AddEquipment(equipment);
        }

        public IQueryable<EquipmentBasicVM> GetListEquipmentVM()
        {
            return _equipment.GetListEquipment();
        }

        public IQueryable<EquipmentBasicVM> SearchListEquipmentVM(string eName)
        {
            return _equipment.SearchListEquipment(eName.Trim());
        }
    }
    public interface IEquipmentService
    {
        IQueryable<EquipmentBasicVM> GetListEquipmentVM();
        IQueryable<EquipmentBasicVM> SearchListEquipmentVM(string eName);
        Task AddEquipmentSV(EquipmentInfoVM equipment);
    }
}
