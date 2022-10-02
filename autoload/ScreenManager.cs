using Godot;
using System;

namespace LudumDare51.AutoLoad
{
    public class ScreenManager : Node
    {
        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event.IsActionPressed("fullscreen"))
            {
                OS.WindowFullscreen = !OS.WindowFullscreen;
            }
        }
    }
}