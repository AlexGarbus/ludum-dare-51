using Godot;
using System;

namespace LudumDare51.UserInterface
{
    public class FightTimer : Control
    {
        private Label _timeLabel;

        private SceneTreeTimer _timer;

        public override void _Ready()
        {
            _timeLabel = GetNode<Label>("%TimeLabel");
        }

        public override void _Process(float delta)
        {
            if (_timer != null)
            {
                _timeLabel.Text = ((int)_timer.TimeLeft).ToString();
            }
        }

        public void Initialize(SceneTreeTimer timer)
        {
            _timer = timer;
        }
    }
}