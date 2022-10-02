using Godot;
using LudumDare51.AutoLoad;

namespace LudumDare51.UserInterface
{
    public class BestScoreLabel : Label
    {
        public override void _Ready()
        {
            Text = $"Fastest knockout: {SaveManager.CurrentSave.BestScore} rounds";
        }
    }
}