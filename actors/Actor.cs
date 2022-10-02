using LudumDare51.AutoLoad;
using Godot;
using System;

namespace LudumDare51.Actors
{
    public class Actor : Area2D
    {
        [Signal]
        delegate void HealthChanged(int value);

        protected enum State
        {
            IDLE,
            DODGE,
            PUNCH,
            KNOCKBACK,
            DEFEAT,
        }

        [Export(PropertyHint.Range, "0,100,or_greater")]
        private int _healAmount;

        [Export(PropertyHint.Range, "1,100,or_greater")]
        private int _damageTaken = 1;

        [Export(PropertyHint.Range, "0,100,or_greater")]
        private float _punchDistance;

        [Export(PropertyHint.Range, "0,100,or_greater")]
        private float _punchTime;

        [Export(PropertyHint.Range, "0,100,or_greater")]
        private float _knockbackDistance;

        [Export(PropertyHint.Range, "0,100,or_greater")]
        private float _knockbackTime;

        public int Health
        {
            get { return _health; }
            set
            {
                _health = Mathf.Clamp(value, 0, _maxHealth);
                EmitSignal(nameof(HealthChanged), _health);
            }
        }

        protected SceneTreeTween MoveTween
        {
            get { return _moveTween; }
            set
            {
                _moveTween?.Kill();
                _moveTween = value;
            }
        }

        protected State _state = State.IDLE;

        protected AnimationPlayer _animationPlayer;

        private AudioStreamPlayer _knockbackSound;
        private AudioStreamPlayer _punchSound;

        protected CollisionShape2D _bodyShape;

        protected FightData _fightData;

        protected Position2D _fistPivot;

        protected Vector2 _idlePosition;

        private bool _isPunchFlipped = false;

        private int _health = 0;
        private int _maxHealth;

        private CollisionShape2D _fistShape;

        private SceneTreeTween _moveTween;

        public override void _Ready()
        {
            _animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
            _knockbackSound = GetNode<AudioStreamPlayer>("%KnockbackSound");
            _punchSound = GetNode<AudioStreamPlayer>("%PunchSound");
            _bodyShape = GetNode<CollisionShape2D>("%BodyShape");
            _fightData = GetNode<FightData>(AutoLoadPaths.FIGHT_DATA_PATH);
            _fistPivot = GetNode<Position2D>("%FistPivot");
            _fistShape = GetNode<CollisionShape2D>("%FistShape");

            _idlePosition = Position;
            _maxHealth = FightData.MAX_HEALTH;
            Health += _healAmount;

            Connect("area_entered", this, nameof(OnAreaEntered));
        }
        
        protected SceneTreeTween CreatePingPongMoveTween(Vector2 displacement, float time)
        {
            SceneTreeTween tween = CreateTween();
            tween.TweenProperty(this, "position", _idlePosition + displacement, time);
            tween.TweenCallback(_animationPlayer, "play", new Godot.Collections.Array("idle", time));
            tween.TweenProperty(this, "position", _idlePosition, time);
            tween.TweenCallback(this, nameof(Idle));
            return tween;
        }

        protected virtual void Idle()
        {
            _state = State.IDLE;

            _bodyShape.SetDeferred("disabled", false);
            _fistShape.SetDeferred("disabled", true);
        }

        protected virtual void Punch(Vector2 direction)
        {
            _state = State.PUNCH;
            _animationPlayer.Play("punch", _punchTime);
            _punchSound.Play();

            FlipFistPivot(_isPunchFlipped);
            _isPunchFlipped = !_isPunchFlipped;

            _fistShape.SetDeferred("disabled", false);

            MoveTween = CreatePingPongMoveTween(_punchDistance * direction, _punchTime);
        }

        protected virtual void Knockback(Vector2 direction)
        {
            _state = State.KNOCKBACK;
            _animationPlayer.Play("knockback", _knockbackTime);
            _knockbackSound.Play();

            MoveTween = CreatePingPongMoveTween(_knockbackDistance * direction, _knockbackTime);
        }

        protected virtual void Defeat()
        {
            _state = State.DEFEAT;
            _animationPlayer.Play("defeat");

            _bodyShape.SetDeferred("disabled", true);
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

        private void OnAreaEntered(Area2D area)
        {
            if (_state == State.KNOCKBACK)
            {
                return;
            }

            Health -= _damageTaken;
            if (Health == 0)
            {
                Defeat();
            }
            else
            {
                Knockback((GlobalPosition - area.GlobalPosition).Normalized());
            }
        }
    }
}