using LudumDare51.Actors;
using Godot;
using System;

namespace LudumDare51.UserInterface
{
    public class FightDisplay : Control
    {
        private HealthBar _playerHealthBar;
        private HealthBar _enemyHealthBar;

        public override void _Ready()
        {
            _playerHealthBar = GetNode<HealthBar>("%PlayerHealthBar");
            _enemyHealthBar = GetNode<HealthBar>("%EnemyHealthBar");
        }

        public void Initialize(Player player, Enemy enemy)
        {
            _playerHealthBar.Initialize(player);
            _enemyHealthBar.Initialize(enemy);
        }
    }
}