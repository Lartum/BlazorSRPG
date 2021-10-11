using BlazorSRPG.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorSRPG.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var res = await _http.PostAsJsonAsync("api/auth/login", request);

            return await res.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegister request)
        {
            var res = await _http.PostAsJsonAsync("api/auth/register", request);

            return await res.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}
