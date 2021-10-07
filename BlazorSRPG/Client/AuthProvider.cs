using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorSRPG.Client
{
    public class AuthProvider : AuthenticationStateProvider
    {
        private ILocalStorageService _localStorageService;

        public AuthProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var state = new AuthenticationState(new ClaimsPrincipal());
            if (await _localStorageService.GetItemAsync<bool>("isAuthenticated"))
            {
                var identity = new ClaimsIdentity(new[]{
                new Claim(ClaimTypes.Name, "Bob") 
                }, "Test authentication type");
                var user = new ClaimsPrincipal(identity);
                state = new AuthenticationState(user);
               
            }
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }
    }
}
