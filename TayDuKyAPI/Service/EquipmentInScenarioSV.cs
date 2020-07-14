using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TayDuKyAPI.Repository;
using TayDuKyAPI.ViewModel;

namespace TayDuKyAPI.Service
{
    public class EquipmentInScenarioSV : IEquipmentInScenarioSV
    {
        private IEquipmentInScenarioRepo _equipRepo;

        public EquipmentInScenarioSV(IEquipmentInScenarioRepo equipRepo)
        {
            this._equipRepo = equipRepo;
        }

        public async Task<bool> AddEquipmentInScenarioSV(EquipInScenarioAddModel addModel)
        {
            if (await _equipRepo.AddEquipmentInScenario(addModel))
            {
                return await _equipRepo.SubEquipmentInScenario(addModel.EquipmentId, addModel.EquipmentQuantity);
            }
            else return false;
        }

        public IQueryable<EquipmentInScenarioDetailVM> GetEquipmentInScenarioByIDSV(int id)
        {
            return _equipRepo.GetEquipmentInScenarioByID(id);

        }

        public IQueryable<EquipInScenarioListModel> GetListEquipmentInScenarioALLSV()
        {
            return _equipRepo.GetListEquipmentInScenarioALL();
        }

        public IQueryable<EquipInScenarioListModel> GetListEquipmentInScenarioSV(int scenarioID)
        {
            return _equipRepo.GetListEquipmentInScenario(scenarioID);
        }

        public Task<int> UpdateEquipmentISCSV(int id, EquipInScenarioAddModel addModel)
        {
            return _equipRepo.UpdateEquipmentISC(id,addModel);

        }
    }
    public interface IEquipmentInScenarioSV {
        IQueryable<EquipInScenarioListModel> GetListEquipmentInScenarioSV(int scenarioID);
        IQueryable<EquipInScenarioListModel> GetListEquipmentInScenarioALLSV();
        Task<bool> AddEquipmentInScenarioSV(EquipInScenarioAddModel addModel);
        Task<int> UpdateEquipmentISCSV(int id, EquipInScenarioAddModel addModel);
        IQueryable<EquipmentInScenarioDetailVM> GetEquipmentInScenarioByIDSV(int id);

    }
}
