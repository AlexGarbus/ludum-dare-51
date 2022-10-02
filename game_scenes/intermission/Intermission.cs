using LudumDare51.AutoLoad;
using LudumDare51.UserInterface;
using Godot;
using System;

namespace LudumDare51.GameScenes
{
    public class Intermission : Node
    {
        [Export(PropertyHint.MultilineText)]
        private string _dialogue;

        private string[] _lines;

        private DialogueBox _dialogueBox;
        private SceneChangeLabel _sceneChangeLabel;

        public override void _Ready()
        {
            _dialogueBox = GetNode<DialogueBox>("%DialogueBox");
            _sceneChangeLabel = GetNode<SceneChangeLabel>("%SceneChangeLabel");

            _lines = _dialogue.Split('\n');

            int round = GetNode<FightData>(AutoLoadPaths.FIGHT_DATA_PATH).Round;
            _dialogueBox.Print(_lines[round >= _lines.Length ? _lines.Length - 1 : round]);
        }

        private void OnDialogueBoxPrintFinished()
        {
            _sceneChangeLabel.Visible = true;
        }
    }
}