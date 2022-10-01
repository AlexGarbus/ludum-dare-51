using LudumDare51.Actors;
using Godot;
using System;

namespace LudumDare51.UserInterface
{
    public class FightDisplay : Control
    {
        private HealthBar _playerHealthBar;
        private HealthBar _enemyHealthBar;
        private FightTimer _fightTimer;

        public override void _Ready()
        {
            _playerHealthBar = GetNode<HealthBar>("%PlayerHealthBar");
            _enemyHealthBar = GetNode<HealthBar>("%EnemyHealthBar");
            _fightTimer = GetNode<FightTimer>("%FightTimer");
        }

        public void Initialize(Player player, Enemy enemy, SceneTreeTimer fightTimer)
        {
            _playerHealthBar.Initialize(player);
            _enemyHealthBar.Initialize(enemy);
            _fightTimer.Initialize(fightTimer);
        }
    }
}