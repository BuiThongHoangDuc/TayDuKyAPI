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


        //private bool EquipmentExists(int id)
        //{
        //    return _context.Equipments.Any(e => e.EquipmentId == id);
        //}

        //    GET: api/Equipments/5
        //    [HttpGet("{id}")]
        //    public async Task<ActionResult<Equipment>> GetEquipment(int id)
        //    {
        //        var equipment = await _context.Equipments.FindAsync(id);

        //        if (equipment == null)
        //        {
        //            return NotFound();
        //        }

        //        return equipment;
        //    }

        //    PUT: api/Equipments/5
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutEquipment(int id, Equipment equipment)
        //    {
        //        if (id != equipment.EquipmentId)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(equipment).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EquipmentExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

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
