using Godot;
using System;

namespace LudumDare51.Actors
{
    public class Enemy : Actor
    {
        [Export(PropertyHint.Range, "1,100,or_greater")]
        private int _idleToPunchChance = 100;

        [Export(PropertyHint.Range, "1,100,or_greater")]
        private int _knockbackToPunchChance = 5;

        [Export(PropertyHint.Range, "0,100,or_greater")]
        private float _windupDistance;

        [Export(PropertyHint.Range, "0,100,or_greater")]
        private float _windupTime;

        public override void _Ready()
        {
            base._Ready();

            Health += _fightData.EnemyHealth;
        }

        public override void _Process(float delta)
        {
            if (_state == State.IDLE && GD.Randi() % _idleToPunchChance == 0)
            {
                WindupPunch();
            }
        }

        protected override void Knockback(Vector2 direction)
        {
            base.Knockback(direction);

            MoveTween.TweenCallback(this, nameof(DetermineKnockbackToPunch));
        }

        private void WindupPunch()
        {
            _state = State.PUNCH;
            _animationPlayer.Play("windup");

            _bodyShape.SetDeferred("disabled", true);

            MoveTween = CreateTween();
            MoveTween.TweenProperty(this, "position", _idlePosition + Vector2.Up * _windupDistance, _windupTime);
            MoveTween.TweenCallback(this, nameof(Punch), new Godot.Collections.Array(Vector2.Down));
        }

        private void DetermineKnockbackToPunch()
        {
            if (GD.Randi() % _knockbackToPunchChance == 0)
            {
                WindupPunch();
            }
        }
    }
}