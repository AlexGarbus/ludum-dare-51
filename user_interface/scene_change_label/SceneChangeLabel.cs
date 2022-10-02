using Godot;
using System;

namespace LudumDare51.UserInterface
{
    public class SceneChangeLabel : Label
    {
        [Export(PropertyHint.File, "*.tscn")]
        private string _scene_path;

        private bool _scene_loaded = false;

        public override void _UnhandledInput(InputEvent @event)
        {
            if (Visible && !_scene_loaded && @event.IsPressed())
            {
                _scene_loaded = true;
                GetTree().ChangeScene(_scene_path);
            }
        }
    }
}