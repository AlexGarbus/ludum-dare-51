using Godot;
using System;

namespace LudumDare51.Actors
{
    public class Actor : Area2D
    {
        [Export(PropertyHint.Range, "1,100,or_greater")]
        private int _maxHealth = 10;

        public int Health
        {
            get { return _health; }
            set
            {
                _health = Mathf.Clamp(value, 0, _maxHealth);
            }
        }

        private int _health;

        //protected AnimationNodeStateMachinePlayback _animationState;
        //protected AnimationTree _animationTree;
        protected AnimationPlayer _animationPlayer;

        protected Position2D _fistPivot;

        protected Vector2 _idlePosition;

        public override void _Ready()
        {
            //_animationTree = GetNode<AnimationTree>("%AnimationTree");
            _animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
            _fistPivot = GetNode<Position2D>("%FistPivot");

            _idlePosition = Position;
            Health = _maxHealth;

            //_animationState = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
        }

        protected void Punch()
        {
            // TODO: Implement
        }
    }
}