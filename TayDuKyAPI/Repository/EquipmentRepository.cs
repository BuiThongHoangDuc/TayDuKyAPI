using Microsoft.EntityFrameworkCore;
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

        public async Task AddEquipment(EquipmentInfoVM equipment)
        {
            Equipment equipmentModel = new Equipment();
            equipmentModel.EquipmentName = equipment.EquipmentName;
            equipmentModel.EquipmentImage = equipment.EquipmentImage;
            equipmentModel.EquipmentDes = equipment.EquipmentDes;
            equipmentModel.EquipmentQuantity = equipment.EquipmentQuantity;
            equipmentModel.EquipmentStatus = Status.AVAILABLE;
            equipmentModel.EquipmentIsDelete = IsDelete.ACTIVE;

            _context.Equipments.Add(equipmentModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<bool> DeleteEquipment(int id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null)
            {
                return false;
            }
            try
            {
                equipment.EquipmentIsDelete = IsDelete.ISDELETED;
                _context.Entry(equipment).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public IQueryable<EquipmentInfoVM> GetEquipment(int id)
        {
            var equipment = _context.Equipments
                                    .Where(equip => equip.EquipmentId == id && equip.EquipmentIsDelete == IsDelete.ACTIVE)
                                    .Select(eq => new EquipmentInfoVM
                                    {
                                        EquipmentId = eq.EquipmentId,
                                        EquipmentDes = eq.EquipmentDes,
                                        EquipmentImage = eq.EquipmentImage,
                                        EquipmentName = eq.EquipmentName,
                                        EquipmentQuantity = eq.EquipmentQuantity,
                                    });
            return equipment;
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
        Task AddEquipment(EquipmentInfoVM equipment);
        Task<bool> DeleteEquipment(int id);
        IQueryable<EquipmentInfoVM> GetEquipment(int id);
    }
}
