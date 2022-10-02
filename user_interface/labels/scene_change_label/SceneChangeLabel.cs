using Godot;

namespace LudumDare51.UserInterface
{
    public class SceneChangeLabel : Label
    {
        [Export(PropertyHint.File, "*.tscn")]
        private string _scene_path;

        private bool _scene_loaded = false;

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event.IsAction("quit") || @event.IsAction("fullscreen"))
            {
                return;
            }    

            if (Visible && !_scene_loaded && IsEventKeyOrButtonPress(@event))
            {
                _scene_loaded = true;
                GetTree().ChangeScene(_scene_path);
            }
        }

        private bool IsEventKeyOrButtonPress(InputEvent @event)
        {
            InputEventKey inputEventKey = @event as InputEventKey;
            InputEventJoypadButton inputEventJoypadButton = @event as InputEventJoypadButton;

            return inputEventKey != null && inputEventKey.Pressed && !inputEventKey.IsEcho() 
                || inputEventJoypadButton != null && inputEventJoypadButton.Pressed;
        }
    }
}