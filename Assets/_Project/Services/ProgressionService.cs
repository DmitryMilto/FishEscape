using _Project.Core.Utils;

namespace _Project.Services
{
    public class ProgressionService
    {
        public int Level { get; private set; } = 1;
        public int Experience { get; private set; } = 0;

        private int[] xpThresholds = { 0, 100, 250, 500, 1000 }; // пример

        public void Load(int level, int experience)
        {
            Level = level;
            Experience = experience;
            TDebug.Log($"[ProgressionService] Loaded level {Level}, XP: {Experience}");
        }

        public void AddExperience(int amount)
        {
            Experience += amount;
            TDebug.Log($"[ProgressionService] Gained XP: {amount}. Total: {Experience}");

            while (Level < xpThresholds.Length && Experience >= xpThresholds[Level])
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            TDebug.Log($"[ProgressionService] Level Up! Now at level {Level}");
        }
    }
}