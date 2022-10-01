using LudumDare51.AutoLoad;
using LudumDare51.UserInterface;
using Godot;
using System;

namespace LudumDare51
{
    public class Intermission : Node
    {
        private const string FIGHT_PATH = "res://fight/fight.tscn";

        [Export(PropertyHint.MultilineText)]
        private string _dialogue;

        private string[] _lines;

        private DialogueBox _dialogueBox;
        private Label _inputLabel;

        public override void _Ready()
        {
            _dialogueBox = GetNode<DialogueBox>("%DialogueBox");
            _inputLabel = GetNode<Label>("%InputLabel");

            _lines = _dialogue.Split('\n');

            int round = GetNode<FightData>(AutoLoadPaths.FIGHT_DATA_PATH).Round;
            _dialogueBox.Print(_lines[round >= _lines.Length ? _lines.Length - 1 : round]);
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (_inputLabel.Visible && (@event is InputEventKey || @event is InputEventJoypadButton))
            {
                GetTree().ChangeScene(FIGHT_PATH);
            }
        }

        private void OnDialogueBoxPrintFinished()
        {
            _inputLabel.Visible = true;
        }
    }
}