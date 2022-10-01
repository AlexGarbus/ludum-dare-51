using Godot;
using System;

namespace LudumDare51.AutoLoad
{
    public class FightData : Node
    {
        public const int MAX_HEALTH = 10;

        public int PlayerHealth { get; set; } = MAX_HEALTH;
        public int EnemyHealth { get; set; } = MAX_HEALTH;

        public int Round { get; set; } = 0;

        public void Reset()
        {
            PlayerHealth = MAX_HEALTH;
            EnemyHealth = MAX_HEALTH;
            Round = 0;
        }
    }
}