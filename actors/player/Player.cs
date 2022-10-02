using Godot;

namespace LudumDare51.Actors
{
    public class Player : Actor
    {
        [Export(PropertyHint.Range, "0,100,or_greater")]
        private float _dodgeDistance;

        [Export(PropertyHint.Range, "0,100,or_greater")]
        private float _dodgeTime;

        private AudioStreamPlayer _dodgeSound;

        public override void _Ready()
        {
            base._Ready();

            _dodgeSound = GetNode<AudioStreamPlayer>("%DodgeSound");

            Health += _fightData.PlayerHealth;
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (_state != State.IDLE)
            {
                return;
            }

            if (@event.IsActionPressed("dodge_left"))
            {
                Dodge(Vector2.Left);
            }
            else if (@event.IsActionPressed("dodge_right"))
            {
                Dodge(Vector2.Right);
            }
            else if (@event.IsActionPressed("punch"))
            {
                Punch(Vector2.Up);
            }
        }

        public override void _ExitTree()
        {
            _fightData.PlayerHealth = Health;
        }

        private void Dodge(Vector2 direction)
        {
            _state = State.DODGE;
            _animationPlayer.Play("dodge", _dodgeTime);
            _dodgeSound.Play();

            FlipFistPivot(direction.x < 0);

            MoveTween = CreatePingPongMoveTween(_dodgeDistance * direction, _dodgeTime);
        }
    }
}