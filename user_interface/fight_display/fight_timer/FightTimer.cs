using Godot;
using System;

namespace LudumDare51.UserInterface
{
    public class FightTimer : Control
    {
        private Label _timeLabel;

        private Timer _timer;

        public override void _Ready()
        {
            _timeLabel = GetNode<Label>("%TimeLabel");
        }

        public override void _Process(float delta)
        {
            if (_timer != null)
            {
                SetTimeLabelText();
            }
        }

        public void Initialize(Timer timer)
        {
            _timer = timer;
            SetTimeLabelText();
        }

        private void SetTimeLabelText()
        {
            _timeLabel.Text = ((int)_timer.TimeLeft).ToString();
        }
    }
}