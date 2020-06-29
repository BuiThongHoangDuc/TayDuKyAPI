using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TayDuKyAPI.Enums;
using TayDuKyAPI.Models;
using TayDuKyAPI.ViewModel;

namespace TayDuKyAPI.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly PRM391Context _context;

        public EquipmentRepository(PRM391Context context)
        {
            _context = context;
        }

        public IQueryable<EquipmentBasicVM> GetListEquipment()
        {
            var listEquipment = _context.Equipments
                                            .Where(eq => eq.EquipmentIsDelete.Equals(IsDelete.ACTIVE))
                                            .Select(eq => new EquipmentBasicVM
                                            {
                                                EquipmentId = eq.EquipmentId,
                                                EquipmentName = eq.EquipmentName,
                                                EquipmentImage = eq.EquipmentImage,
                                                EquipmentDes = eq.EquipmentDes,
                                                EquipmentQuantity = eq.EquipmentQuantity,
                                            });
            return listEquipment;
        }

        public IQueryable<EquipmentBasicVM> SearchListEquipment(string eName)
        {
            var listEquipment = _context.Equipments
                                            .Where(eq => eq.EquipmentName.Contains(eName) && eq.EquipmentIsDelete.Equals(IsDelete.ACTIVE))
                                            .Select(eq => new EquipmentBasicVM
                                            {
                                                EquipmentId = eq.EquipmentId,
                                                EquipmentName = eq.EquipmentName,
                                                EquipmentImage = eq.EquipmentImage,
                                                EquipmentDes = eq.EquipmentDes,
                                                EquipmentQuantity = eq.EquipmentQuantity,
                                            });
            return listEquipment;
        }
    }
    public interface IEquipmentRepository
    {
        IQueryable<EquipmentBasicVM> GetListEquipment();
        IQueryable<EquipmentBasicVM> SearchListEquipment(string eName);
    }
}
