using BlazorSRPG.Server.Data;
using BlazorSRPG.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSRPG.Server.Controllers
{
    [Route("api/units")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly DataContext _context;
        public UnitController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUnits()
        {
            var units = await _context.Units.ToListAsync();
            return Ok(units);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddUnit(Unit unit)
        {
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();
            return Ok(await _context.Units.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(int id, Unit unit)
        {
            var dbUnit = await _context.Units.FirstOrDefaultAsync(u => u.Id == id);
          /*  foreach(var property in typeof(Unit).GetProperties())
            {
      
              if (property.PropertyType == typeof(String))
                {

                }
            }*/

            //creating checks to update only the fields needed to be updated 
            if(dbUnit == null)
            {
                return NotFound("The provided Id is invalid");
            }
            if (unit.Title.Length > 0)
            {
                dbUnit.Title = unit.Title;
            }
            if(unit.Attack != 0)
            {
                dbUnit.Attack = unit.Attack;
            }
            if (unit.Defense != 0)
            {
                dbUnit.Defense = unit.Defense;
            }
            if (unit.Hitpoints != 0)
            {
                dbUnit.Hitpoints = unit.Hitpoints;
            }
            if (unit.BananaCost != 0)
            {
                dbUnit.BananaCost = unit.BananaCost;
            }
            await _context.SaveChangesAsync();
            return Ok(dbUnit);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var dbUnit = await _context.Units.FirstOrDefaultAsync(u => u.Id == id);
            //creating checks to update only the fields needed to be updated 
            if (dbUnit == null)
            {
                return NotFound("The provided Id is invalid");
            }
            
            _context.Remove(dbUnit);
            await _context.SaveChangesAsync();
            return Ok(await _context.Units.ToListAsync());
        }
    }
}
