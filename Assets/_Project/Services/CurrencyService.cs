using _Project.Core.Utils;

namespace _Project.Services
{
    public class CurrencyService
    {
        public int Gold { get; private set; }
        public int Crystals { get; private set; }

        public void Load(int gold, int crystals)
        {
            Gold = gold;
            Crystals = crystals;
            TDebug.Log($"[CurrencyService] Loaded: {Gold} gold, {Crystals} crystals.");
        }

        public void AddGold(int amount)
        {
            Gold += amount;
            TDebug.Log($"[CurrencyService] Added gold: {amount}. Total: {Gold}");
        }

        public bool SpendGold(int amount)
        {
            if (Gold < amount)
            {
                TDebug.LogWarning("[CurrencyService] Not enough gold.");
                return false;
            }

            Gold -= amount;
            TDebug.Log($"[CurrencyService] Spent gold: {amount}. Left: {Gold}");
            return true;
        }

        public void AddCrystals(int amount)
        {
            Crystals += amount;
            TDebug.Log($"[CurrencyService] Added crystals: {amount}. Total: {Crystals}");
        }

        public bool SpendCrystals(int amount)
        {
            if (Crystals < amount)
            {
                TDebug.LogWarning("[CurrencyService] Not enough crystals.");
                return false;
            }

            Crystals -= amount;
            TDebug.Log($"[CurrencyService] Spent crystals: {amount}. Left: {Crystals}");
            return true;
        }
    }
}