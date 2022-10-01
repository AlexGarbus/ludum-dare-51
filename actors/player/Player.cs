using Godot;
using System;

namespace LudumDare51.Actors
{
    public class Player : Actor
    {
        private enum State
        {
            IDLE,
            DODGE,
            PUNCH,
            HIT,
        }

        [Export(PropertyHint.Range, "0,100,or_greater")]
        private float _dodgeDistance;

        [Export(PropertyHint.Range, "0,100,or_greater")]
        private float _dodgeTime;
        
        private State _state = State.IDLE;

        public override void _UnhandledInput(InputEvent @event)
        {
            if (_state != State.IDLE)
            {
                return;
            }

            if (@event.IsActionPressed("dodge_left"))
            {
                Dodge(new Vector2(-1, 0));
            }
            else if (@event.IsActionPressed("dodge_right"))
            {
                Dodge(new Vector2(1, 0));
            }
            else if (@event.IsActionPressed("punch"))
            {
                Punch();
            }
        }

        private void Idle()
        {
            _state = State.IDLE;
        }

        private void Dodge(Vector2 direction)
        {
            _state = State.DODGE;
            _animationPlayer.Play("dodge", _dodgeTime);

            Vector2 pivotScale = new Vector2(1, 1);
            if (direction.x < 0)
            {
                pivotScale.x = -1;
            }
            _fistPivot.Scale = pivotScale;

            SceneTreeTween tween = GetTree().CreateTween();
            tween.TweenProperty(this, "position", _idlePosition + direction * _dodgeDistance, _dodgeTime);
            tween.TweenCallback(_animationPlayer, "play", new Godot.Collections.Array("idle", _dodgeTime));
            tween.TweenProperty(this, "position", _idlePosition, _dodgeTime);
            tween.TweenCallback(this, "Idle");
        }
    }
}