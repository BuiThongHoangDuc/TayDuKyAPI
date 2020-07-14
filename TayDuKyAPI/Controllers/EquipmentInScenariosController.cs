using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TayDuKyAPI.Models;
using TayDuKyAPI.Service;
using TayDuKyAPI.ViewModel;

namespace TayDuKyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentInScenariosController : ControllerBase
    {
        private readonly IEquipmentInScenarioSV _eis;

        public EquipmentInScenariosController(IEquipmentInScenarioSV eis)
        {
            _eis = eis;
        }

        // GET: api/EquipmentInScenarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipInScenarioListModel>>> GetEquipmentInScenarios()
        {
            var listEquipmentInSC = await _eis.GetListEquipmentInScenarioALLSV().ToListAsync();
            if (listEquipmentInSC.Count == 0) return NotFound();
            else return Ok(listEquipmentInSC);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentInScenarioDetailVM>> GetEquipmentInScenarioByID(int id)
        {
            var eis = await _eis.GetEquipmentInScenarioByIDSV(id).FirstOrDefaultAsync();

            if (eis == null)
            {
                return NotFound();
            }

            return Ok(eis);
        }
    }
}
