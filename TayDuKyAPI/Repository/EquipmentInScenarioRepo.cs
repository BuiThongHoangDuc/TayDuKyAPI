using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TayDuKyAPI.Enums;
using TayDuKyAPI.Models;
using TayDuKyAPI.ViewModel;

namespace TayDuKyAPI.Repository
{
    public class EquipmentInScenarioRepo : IEquipmentInScenarioRepo
    {
        private readonly PRM391Context _context;

        public EquipmentInScenarioRepo(PRM391Context context)
        {
            _context = context;
        }

        public async Task<bool> AddEquipmentInScenario(EquipInScenarioAddModel addModel)
        {
            bool check = await checkQuantity(addModel.EquipmentId, addModel.EquipmentQuantity);
            if (check == false) {
                return false;
            }
            else
            {
                EquipmentInScenario equipInScenario = new EquipmentInScenario();
                equipInScenario.ScenarioId = addModel.ScenarioId;
                equipInScenario.EquipmentId = addModel.EquipmentId;
                equipInScenario.EquipmentQuantity = addModel.EquipmentQuantity;
                equipInScenario.CreateByDate = addModel.CreateByDate;
                equipInScenario.UpdateByDate = addModel.UpdateByDate;
                equipInScenario.PersonUpdate = addModel.PersonUpdate;
                equipInScenario.IsDelete = 0;


                _context.EquipmentInScenarios.Add(equipInScenario);
                try
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateException e)
                {
                    Debug.WriteLine(e.InnerException.Message);
                    throw;
                }
            }
        }

        private async Task<bool> checkQuantity(int equipmentID,int quantity) {
            int quantityEquip = await _context.Equipments
                                            .Where(eq => eq.EquipmentId == equipmentID)
                                            .Select(eq => eq.EquipmentQuantity).FirstOrDefaultAsync();
            if (quantity > quantityEquip) return false;
            else return true;
        }
        public IQueryable<EquipInScenarioListModel> GetListEquipmentInScenario(int scenarioID)
        {
            var actorInSC = _context.EquipmentInScenarios
                                            .Where(eis => eis.ScenarioId == scenarioID && eis.IsDelete == IsDelete.ACTIVE)
                                            .Select(eis => new EquipInScenarioListModel
                                            {
                                                EquipInScenario = eis.EquipInScenario,
                                                EquipmentImage = eis.Equipment.EquipmentImage,
                                                EquipmentName = eis.Equipment.EquipmentName,
                                                EquipmentQuantity = eis.EquipmentQuantity.Value,
                                                PersonUpdate = eis.PersonUpdate,
                                                UpdateByDate = eis.UpdateByDate,
                                            }) ;
            return actorInSC;
        }

        public IQueryable<EquipInScenarioListModel> GetListEquipmentInScenarioALL()
        {
            var actorInSC = _context.EquipmentInScenarios
                                            .Where(eis => eis.IsDelete == IsDelete.ACTIVE)
                                            .Select(eis => new EquipInScenarioListModel
                                            {
                                                EquipInScenario = eis.EquipInScenario,
                                                EquipmentImage = eis.Equipment.EquipmentImage,
                                                ScenarioName = eis.Scenario.ScenarioName,
                                                EquipmentName = eis.Equipment.EquipmentName,
                                                EquipmentQuantity = eis.EquipmentQuantity.Value,
                                                ScenarioTimeFrom = eis.Scenario.ScenarioTimeFrom,
                                                ScenarioTimeTo = eis.Scenario.ScenarioTimeTo,
                                                PersonUpdate = eis.PersonUpdate,
                                                UpdateByDate = eis.UpdateByDate,
                                            });
            return actorInSC;
        }

        public async Task<bool> SubEquipmentInScenario(int id ,int quantity)
        {
            Equipment equipmentModel = await _context.Equipments.FindAsync(id);
            if (equipmentModel == null) return false;
            equipmentModel.EquipmentQuantity = equipmentModel.EquipmentQuantity - quantity;
            _context.Entry(equipmentModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<bool> DeleteEquipmentISC(int id)
        {
            var equipmentISC = await _context.EquipmentInScenarios.FindAsync(id);
            if (equipmentISC == null)
            {
                return false;
            }
            try
            {
                equipmentISC.IsDelete = IsDelete.ISDELETED;
                _context.Entry(equipmentISC).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<int> UpdateEquipmentISC(int id, EquipInScenarioAddModel addModel)
        {
            bool check = await checkQuantity(addModel.EquipmentId, addModel.EquipmentQuantity);
            if (check == false)
            {
                return -1;
            }

            EquipmentInScenario equipmentISCModel = await _context.EquipmentInScenarios.FindAsync(id);
            if (equipmentISCModel == null) return -1;
            equipmentISCModel.ScenarioId = addModel.ScenarioId;
            equipmentISCModel.EquipmentId = addModel.EquipmentId;
            equipmentISCModel.EquipmentQuantity = addModel.EquipmentQuantity;
            equipmentISCModel.UpdateByDate = addModel.UpdateByDate;
            equipmentISCModel.PersonUpdate = addModel.PersonUpdate;

            _context.Entry(equipmentISCModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return equipmentISCModel.EquipInScenario;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public IQueryable<EquipmentInScenarioDetailVM> GetEquipmentInScenarioByID(int id)
        {
            var ActorRole = _context.EquipmentInScenarios
                                  .Where(eis => eis.EquipInScenario == id && eis.IsDelete == IsDelete.ACTIVE && eis.Equipment.EquipmentIsDelete == IsDelete.ACTIVE)
                                  .Select(eis => new EquipmentInScenarioDetailVM
                                  {
                                      EquipInScenario = eis.EquipInScenario,
                                      CreateByDate = eis.CreateByDate,
                                      EquipmentName = eis.Equipment.EquipmentName,
                                      EquipmentQuantity = eis.EquipmentQuantity,
                                      PersonUpdate = eis.PersonUpdate,
                                      ScenarioName = eis.Scenario.ScenarioName,
                                      UpdateByDate = eis.UpdateByDate,
                                  });
            return ActorRole;
        }
    }

    public interface IEquipmentInScenarioRepo {
        IQueryable<EquipInScenarioListModel> GetListEquipmentInScenario(int scenarioID);
        IQueryable<EquipInScenarioListModel> GetListEquipmentInScenarioALL();
        Task<bool> AddEquipmentInScenario(EquipInScenarioAddModel addModel);
        Task<bool> SubEquipmentInScenario(int id,int quantity);
        Task<bool> DeleteEquipmentISC(int id);
        Task<int> UpdateEquipmentISC(int id, EquipInScenarioAddModel addModel);
        IQueryable<EquipmentInScenarioDetailVM> GetEquipmentInScenarioByID(int id);

    }
}
