using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSRPG.Client.Services
{
    public class BananaService : IBananaService
    {
        public event Action OnChange;
        public int Bananas { get; set; } = 1000;
        void BananasChanges() => OnChange.Invoke();
        public void ConsumeBananas(int amount)
        {
            Bananas -= amount;
            BananasChanges();
        }
        public void AddBananas(int amount)
        {
            Bananas += amount;
            BananasChanges();
        }
    }
}
