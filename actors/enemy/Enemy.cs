using Godot;
using System;

namespace LudumDare51.Actors
{
    public class Enemy : Actor
    {
        [Export(PropertyHint.Range, "0,100,or_greater")]
        private int _punchChance = 100;

        public override void _Ready()
        {
            base._Ready();

            Health += _fightData.EnemyHealth;
        }

        public override void _Process(float delta)
        {
            if (_state == State.IDLE && GD.Randi() % _punchChance == 0)
            {
                Punch(Vector2.Down);
            }
        }
    }
}