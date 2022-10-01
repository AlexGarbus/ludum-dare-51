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
        private string _dialogueText;

        private string[] _lines;

        private Dialogue _dialogue;

        public override void _Ready()
        {
            _dialogue = GetNode<Dialogue>("%Dialogue");

            _lines = _dialogueText.Split('\n');

            int round = GetNode<FightData>(AutoLoadPaths.FIGHT_DATA_PATH).Round;
            _dialogue.Print(_lines[round >= _lines.Length ? _lines.Length - 1 : round]);
        }

        private void OnDialoguePrintFinished()
        {
            GetTree().ChangeScene(FIGHT_PATH);
        }
    }
}