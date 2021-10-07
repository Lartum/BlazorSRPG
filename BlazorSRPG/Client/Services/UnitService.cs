using Blazored.Toast.Services;
using BlazorSRPG.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorSRPG.Client.Services
{
    public class UnitService : IUnitService
    {
        private readonly IToastService _toastService;
        private readonly HttpClient _http;
        public UnitService(IToastService toastService, HttpClient http)
        {
            _toastService = toastService;
            _http = http;
        }
        public IList<Unit> Units { get; set; } = new List<Unit>();
        public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>();
       /* public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit> {
         new UserUnit { UnitId = 1, HitPoints = 100}
        };*/
        public void AddUnit(int unitId)
        {
            var unit = Units.First(unit => unit.Id == unitId);
            _toastService.ShowSuccess($"{unit.Title} built!", "Unit created");
            MyUnits.Add(new UserUnit { UnitId = unit.Id, HitPoints = unit.Hitpoints });
        }
        public async Task LoadUnitsAsync()
        {
            Units = await _http.GetFromJsonAsync<IList<Unit>>("api/units");
        }
    }
}
