using LudumDare51.Actors;
using LudumDare51.AutoLoad;
using Godot;
using System;

namespace LudumDare51.UserInterface
{
    public class HealthBar : ProgressBar
    {
        public override void _Ready()
        {
            MaxValue = FightData.MAX_HEALTH;
        }

        public void Initialize(Actor actor)
        {
            Value = actor.Health;
            actor.Connect("HealthChanged", this, nameof(OnActorHealthChanged));
        }

        private void OnActorHealthChanged(int value)
        {
            Value = value;
        }
    }
}