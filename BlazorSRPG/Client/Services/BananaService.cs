using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorSRPG.Client.Services
{
    public class BananaService : IBananaService
    {
        public event Action OnChange;
        private readonly HttpClient _http;
        public BananaService(HttpClient http)
        {
            _http = http;
        }
        public int Bananas { get; set; } = 1000;
        void BananasChanges() => OnChange.Invoke();
        public void ConsumeBananas(int amount)
        {
            Bananas -= amount;
            BananasChanges();
        }
        public async Task AddBananas(int amount)
        {
            var result = await _http.PutAsJsonAsync<int>("api/user/bananas", amount);
            Bananas = await result.Content.ReadFromJsonAsync<int>();
            BananasChanges();
        }

        public async Task GetBananas() 
        {
            Bananas = await _http.GetFromJsonAsync<int>("api/user/bananas");
            BananasChanges();
        }

    }
}
