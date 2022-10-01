using Godot;
using System;

namespace LudumDare51.UserInterface
{
    public class Dialogue : Label
    {
        [Signal]
        public delegate void PrintFinished();

        private int _characters;

        private string _dialogueText;

        private Timer _characterTimer;

        public override void _Ready()
        {
            _characterTimer = GetNode<Timer>("%CharacterTimer");
        }

        public void Print(string dialogueText)
        {
            Text = "";
            _dialogueText = dialogueText;

            _characterTimer.Start();
        }

        private void OnCharacterTimerTimeout()
        {
            Text += _dialogueText[_characters++];

            if (_characters == _dialogueText.Length)
            {
                _characterTimer.Stop();
                EmitSignal(nameof(PrintFinished));
            }
        }
    }
}