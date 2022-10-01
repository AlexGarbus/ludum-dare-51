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
                EmitSignal(nameof(HealthChanged), value);
            }
        }

        protected State _state = State.IDLE;

        protected AnimationPlayer _animationPlayer;

        protected FightData _fightData;

        protected Position2D _fistPivot;

        protected Vector2 _idlePosition;

        private bool _isPunchFlipped = false;

        private int _health = 0;
        private int _maxHealth;

        public override void _Ready()
        {
            _animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
            _fightData = GetNode<FightData>(AutoLoadPaths.FIGHT_DATA_PATH);
            _fistPivot = GetNode<Position2D>("%FistPivot");

            _idlePosition = Position;
            Health += _healAmount;
            _maxHealth = FightData.MAX_HEALTH;

            Connect("area_entered", this, nameof(OnAreaEntered));
        }
        
        protected void Move(Vector2 displacement, float time)
        {
            SceneTreeTween tween = GetTree().CreateTween();
            tween.TweenProperty(this, "position", _idlePosition + displacement, time);
            tween.TweenCallback(_animationPlayer, "play", new Godot.Collections.Array("idle", time));
            tween.TweenProperty(this, "position", _idlePosition, time);
            tween.TweenCallback(this, nameof(Idle));
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

        protected void Knockback(Vector2 direction)
        {
            _state = State.KNOCKBACK;
            _animationPlayer.Play("knockback", _knockbackTime);

            Move(_knockbackDistance * direction, _knockbackTime);
        }

        protected void Defeat()
        {
            _state = State.DEFEAT;
            _animationPlayer.Play("defeat");

            GetNode<CollisionShape2D>("%CollisionShape2D").SetDeferred("disabled", true);
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
            Health--;
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