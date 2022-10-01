using LudumDare51.Actors;
using Godot;
using System;

namespace LudumDare51.UserInterface
{
    public class HealthBar : ProgressBar
    {
        public void Initialize(Actor actor)
        {
            MaxValue = actor.Health;
            Value = MaxValue;
            actor.Connect("HealthChanged", this, nameof(OnActorHealthChanged));
        }

        private void OnActorHealthChanged(int value)
        {
            Value = value;
        }
    }
}