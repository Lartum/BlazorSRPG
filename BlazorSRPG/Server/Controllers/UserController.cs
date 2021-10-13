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
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorSRPG.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public UserController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpGet("bananas")]
        public async Task<IActionResult> GetBananas() 
        {
            var user = await _utilityService.GetUser();
            return Ok(user.Bananas);
        }

        [HttpPut("bananas")]
        public async Task<IActionResult> AddBananas([FromBody ] int bananas) 
        {
            var user = await _utilityService.GetUser();
            user.Bananas += bananas;
            await _context.SaveChangesAsync();
            return Ok(user.Bananas);
        }

        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard() 
        {
            var users = await _context.Users.Where(user => !user.IsDeleted && user.IsConfirmed).ToListAsync();
            users = users
                    .OrderByDescending(user => user.Victories)
                    .ThenBy(user => user.Defeats)
                    .ThenBy(user => user.CreatedAt)
                    .ToList();
            int rank = 1;
            var response = users.Select(user => new UserStatistic
            {
                Rank = rank++,
                UserId = user.Id,
                Username = user.Username,
                Battles = user.Battles,
                Victories = user.Victories,
                Defeats = user.Defeats
            });;
            return Ok(response);
        }
    }
}
