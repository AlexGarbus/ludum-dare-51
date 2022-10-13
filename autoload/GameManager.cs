using Godot;
using System;

namespace LudumDare51.AutoLoad
{
    public class GameManager : Node
    {
        private readonly string[] _game_scene_names = { "Title", "Intermission", "Fight", "Results" };

        public override void _Ready()
        {
            GetTree().Connect("node_added", this, nameof(OnNodeAdded));
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (!OS.GetName().Equals("HTML5") && @event.IsActionPressed("quit"))
            {
                GetTree().Quit();
            }
        }

        private void OnNodeAdded(Node node)
        {
            if (Array.IndexOf(_game_scene_names, node.Name) != -1)
            {
                GetTree().Paused = false;
            }
        }
    }
}