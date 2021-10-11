using BlazorSRPG.Server.Data;
using BlazorSRPG.Server.Services;
using BlazorSRPG.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSRPG.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserUnitController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public UserUnitController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpPost]
        public async Task<IActionResult> BuildUserUnit ([FromBody] int unitId) {
            var unit = _context.Units.FirstOrDefault<Unit>(u => u.Id == unitId);
            var user = await _utilityService.GetUser();
            if(user.Bananas < unit.BananaCost)
            {
                return BadRequest("Not Enough Bananas");
            }
            user.Bananas -= unit.BananaCost;

            var newUserUnit = new UserUnit
            {
                UnitId = unit.Id,
                UserId = user.Id,
                HitPoints = unit.Hitpoints
            };
            _context.UserUnits.Add(newUserUnit);
            await _context.SaveChangesAsync();
            return Ok(newUserUnit);
        }

        [HttpGet]
        public async Task<IActionResult> GeUserUnits()
        {
            var user = await _utilityService.GetUser();
            
            var userUnits = await _context.UserUnits.Where(unit => unit.UserId == user.Id).ToListAsync();
            
            var response = userUnits.Select(
            unit => new UserUnitResponse
            {
                UnitId = unit.UnitId,
                Hitpoints = unit.HitPoints
            });
            return Ok(response);
        }
    }
}
