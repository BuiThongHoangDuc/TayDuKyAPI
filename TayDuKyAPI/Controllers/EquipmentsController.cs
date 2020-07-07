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
    public class EquipmentsController : ControllerBase
    {
        private readonly IEquipmentService _equipment;

        public EquipmentsController(IEquipmentService equipment)
        {
            _equipment = equipment;
        }

        // GET: api/Equipments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentBasicVM>>> SearchEquipments(string eName)
        {
            if (eName == null) return NotFound();
            var list = await _equipment.SearchListEquipmentVM(eName).ToListAsync();
            if (list.Count == 0) return NotFound();
            else return Ok(list);
        }

        // GET: api/Equipments
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<EquipmentBasicVM>>> GetEquipments()
        {
            var list = await _equipment.GetListEquipmentVM().ToListAsync();
            if (list.Count == 0) return NotFound();
            else return Ok(list);
        }

        //POST: api/Equipments
        [HttpPost]
        public async Task<ActionResult> AddEquipment(EquipmentInfoVM equipment)
        {
            try
            {
                await _equipment.AddEquipmentSV(equipment);
            }
            catch (Exception) { return BadRequest(); }
            return NoContent();
        }

        //DELETE: api/Equipments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEquipment(int id)
        {
            try
            {
                var isDelete = await _equipment.DeleteEquipmentSV(id);
                if (isDelete) return NoContent();
                else return NotFound();
            }
            catch (Exception) { return BadRequest(); }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentInfoVM>> GetEquipment(int id)
        {
            var equip = await _equipment.GetEquipmentSV(id).FirstOrDefaultAsync();

            if (equip == null)
            {
                return NotFound();
            }

            return Ok(equip);
        }

        //PUT: api/Equipments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipment(int id, EquipmentInfoVM equipment)
        {
            if (id != equipment.EquipmentId)
            {
                return BadRequest();
            }

            try
            {
                int idUpdate = await _equipment.UpdateEquipmentSV(id, equipment);
                if (idUpdate == -1) return NotFound();
                else return Ok(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //    POST: api/Equipments
        //   [HttpPost]
        //    public async Task<ActionResult<Equipment>> PostEquipment(Equipment equipment)
        //    {
        //        _context.Equipments.Add(equipment);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetEquipment", new { id = equipment.EquipmentId }, equipment);
        //    }


    }
}
