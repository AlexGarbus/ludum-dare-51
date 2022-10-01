using Godot;
using System;

namespace LudumDare51.Actors
{
    public class Actor : Area2D
    {
        protected enum State
        {
            IDLE,
            DODGE,
            PUNCH,
            HIT,
        }

        [Export(PropertyHint.Range, "1,100,or_greater")]
        private int _maxHealth = 10;

        [Export(PropertyHint.Range, "0,100,or_greater")]
        private float _punchDistance;

        [Export(PropertyHint.Range, "0,100,or_greater")]
        private float _punchTime;

        public int Health
        {
            get { return _health; }
            set
            {
                _health = Mathf.Clamp(value, 0, _maxHealth);
            }
        }

        protected State _state = State.IDLE;

        protected AnimationPlayer _animationPlayer;

        protected Position2D _fistPivot;

        protected Vector2 _idlePosition;

        private bool _isPunchFlipped = false;

        private int _health;

        public override void _Ready()
        {
            _animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
            _fistPivot = GetNode<Position2D>("%FistPivot");

            _idlePosition = Position;
            Health = _maxHealth;
        }
        
        protected void Move(Vector2 displacement, float time)
        {
            SceneTreeTween tween = GetTree().CreateTween();
            tween.TweenProperty(this, "position", _idlePosition + displacement, time);
            tween.TweenCallback(_animationPlayer, "play", new Godot.Collections.Array("idle", time));
            tween.TweenProperty(this, "position", _idlePosition, time);
            tween.TweenCallback(this, "Idle");
        }

        protected void Idle()
        {
            _state = State.IDLE;
        }

        protected void Punch(Vector2 direction)
        {
            _state = State.PUNCH;
            _animationPlayer.Play("punch", _punchTime);

            FlipFistPivot(_isPunchFlipped);
            _isPunchFlipped = !_isPunchFlipped;

            Move(_punchDistance * direction, _punchTime);
        }

        protected void FlipFistPivot(bool isFlipped)
        {
            Vector2 scale = new Vector2(1, 1);
            if (isFlipped)
            {
                scale.x = -1;
            }
            _fistPivot.Scale = scale;
        }
    }
}