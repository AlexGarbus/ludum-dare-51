using Godot;
using System;

namespace LudumDare51.AutoLoad
{
    public class GameManager : Node
    {
        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event.IsActionPressed("quit"))
            {
                GetTree().Quit();
            }
        }
    }
}