using LudumDare51.AutoLoad;
using LudumDare51.Saving;
using Godot;
using System;

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