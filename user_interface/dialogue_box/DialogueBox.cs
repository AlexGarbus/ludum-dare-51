using Godot;
using System;

namespace LudumDare51.UserInterface
{
    public class DialogueBox : Label
    {
        [Signal]
        public delegate void PrintFinished();

        private int _characters;

        private string _dialogue;

        private Timer _characterTimer;

        public override void _Ready()
        {
            _characterTimer = GetNode<Timer>("%CharacterTimer");
        }

        public void Print(string dialogue)
        {
            Text = "";
            _dialogue = dialogue;

            _characterTimer.Start();
        }

        private void OnCharacterTimerTimeout()
        {
            Text += _dialogue[_characters++];

            if (_characters == _dialogue.Length)
            {
                _characterTimer.Stop();
                EmitSignal(nameof(PrintFinished));
            }
        }
    }
}