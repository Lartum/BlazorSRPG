using BlazorSRPG.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSRPG.Server.Services
{
    public interface IUtilityService
    {
        Task<User> GetUser();
    }
}
