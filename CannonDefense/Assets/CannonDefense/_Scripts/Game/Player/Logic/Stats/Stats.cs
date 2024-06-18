using System.Collections.Generic;
using GlassyCode.CannonDefense.Game.Player.Data.Stats;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Stats
{
    public struct Stats
    {
        public int Health;
        public int Damage;
        public int Score;
        public int Experience;
        public int Level;

        public bool IsDied => Health <= 0;

        public void AddScore(int score) => Score += score;
        public void AddExperience(int experience) => Experience += experience;
        public void DecreaseHealth(int health) => Health -= health;

        public int GetDamage(IReadOnlyList<int> damageList)
        {
            if (Level - 1 < damageList.Count)
            {
                return damageList[Level - 1];
            }

            Debug.LogWarning($"Level {Level} exceeds the defined damage list. Using the maximum defined damage.");
            return damageList[^1];
        }

        public int LevelUp(StatsData statsData)
        {
            if (!CanLevelUp(statsData)) return 0;

            var levelsGained = 0;

            while (Level < statsData.MaxLevel)
            {
                var requiredExpForLevelUp = GetRequiredExperience(statsData.RequiredExpToLevelUp);
                
                if (requiredExpForLevelUp <= 0) break;
                if (Experience < requiredExpForLevelUp) break;

                Experience -= requiredExpForLevelUp;
                Damage = GetDamage(statsData.Damage);
                levelsGained++;
                Level++;
            }

            return levelsGained;
        }

        private bool CanLevelUp(StatsData statsData)
        {
            if (Level >= statsData.MaxLevel)
            {
                Debug.LogWarning($"Player has reached the maximum level ({statsData.MaxLevel}). Cannot level up further.");
                return false;
            }

            var requiredExperience = GetRequiredExperience(statsData.RequiredExpToLevelUp);
            return requiredExperience > 0 && Experience >= requiredExperience;
        }

        private int GetRequiredExperience(IReadOnlyList<int> requiredExpToLevelUp)
        {
            if (Level < requiredExpToLevelUp.Count) return requiredExpToLevelUp[Level - 1];

            Debug.LogWarning($"Required experience for level {Level} is not defined. Using the maximum value instead.");
            return -1;
        }
    }
}