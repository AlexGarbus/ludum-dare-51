using Godot;
using System;

namespace LudumDare51.Actors
{
    public class Enemy : Actor
    {
        [Export(PropertyHint.Range, "0,100,or_greater")]
        private int _punchChance = 100;

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
            if (_state == State.IDLE && GD.Randi() % _punchChance == 0)
            {
                WindupPunch();
            }
        }

        private void WindupPunch()
        {
            _state = State.PUNCH;
            _animationPlayer.Play("windup");

            SceneTreeTween tween = CreateTween();
            tween.TweenProperty(this, "position", _idlePosition + Vector2.Up * _windupDistance, _windupTime);
            tween.TweenCallback(this, nameof(Punch), new Godot.Collections.Array(Vector2.Down));
        }
    }
}