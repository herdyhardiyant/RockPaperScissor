using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public static class BotDifficulty
    {
        
        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }

        public static float Health => _health;
        public static float AttackDamage => _attackDamage;
        public static Difficulty difficulty => _difficulty;

        private static float _health;
        private static float _attackDamage;
        
        private static Difficulty _difficulty = Difficulty.Easy;

        public static void SetDifficulty(Difficulty difficulty)
        {
            _difficulty = difficulty;
            switch (difficulty)
            {
                case Difficulty.Easy:
                    _health = 80;
                    _attackDamage = -10;
                    break;
                case Difficulty.Medium:
                    _health = 100;
                    _attackDamage = -20;
                    break;
                case Difficulty.Hard:
                    _health = 120;
                    _attackDamage = -30;
                    break;
            }
        }

    }
}