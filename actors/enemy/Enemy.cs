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

        [Export(PropertyHint.Range, "1,100,or_greater")]
        private int _punchToPunchChance = 3;

        [Export(PropertyHint.Range, "0,100,or_greater")]
        private float _windupDistance;

        [Export(PropertyHint.Range, "0,100,or_greater")]
        private float _windupTime;

        private Timer _punchTimer;

        public override void _Ready()
        {
            base._Ready();

            _punchTimer = GetNode<Timer>("%PunchTimer");

            Health += _fightData.EnemyHealth;
        }

        public override void _Process(float delta)
        {
            if (_state == State.IDLE)
            {
                if (_punchTimer.TimeLeft == 0)
                {
                    WindupPunch();
                }
                else
                {
                    DeterminePunch(_idleToPunchChance);
                }
            }
        }

        public override void _ExitTree()
        {
            _fightData.EnemyHealth = Health;
        }

        protected override void Punch(Vector2 direction)
        {
            base.Punch(direction);

            MoveTween.TweenCallback(this, nameof(DeterminePunch), new Godot.Collections.Array(_punchToPunchChance));
            MoveTween.TweenCallback(_punchTimer, "start");
        }

        protected override void Knockback(Vector2 direction)
        {
            base.Knockback(direction);

            MoveTween.TweenCallback(this, nameof(DeterminePunch), new Godot.Collections.Array(_knockbackToPunchChance));
        }

        private void WindupPunch()
        {
            _state = State.PUNCH;
            _animationPlayer.Play("windup");

            _punchTimer.Stop();

            _bodyShape.SetDeferred("disabled", true);

            MoveTween = CreateTween();
            MoveTween.TweenProperty(this, "position", _idlePosition + Vector2.Up * _windupDistance, _windupTime);
            MoveTween.TweenCallback(this, nameof(Punch), new Godot.Collections.Array(Vector2.Down));
        }

        private void DeterminePunch(int chance)
        {
            if (GD.Randi() % chance == 0)
            {
                WindupPunch();
            }
        }
    }
}