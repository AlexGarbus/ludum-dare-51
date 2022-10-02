using LudumDare51.AutoLoad;
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