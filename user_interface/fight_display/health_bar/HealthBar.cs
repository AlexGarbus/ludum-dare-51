using Godot;
using LudumDare51.Actors;
using LudumDare51.AutoLoad;

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