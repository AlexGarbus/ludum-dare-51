using LudumDare51.AutoLoad;
using LudumDare51.UserInterface;
using Godot;
using System;

namespace LudumDare51.GameScenes
{
    public class Results : Control
    {
        [Export]
        private string _winText = "You are winner!";

        [Export]
        private string _loseText = "You are loser!";

        private FightData _fightData;

        private Label _resultsLabel;
        private SceneChangeLabel _sceneChangeLabel;

        public override void _Ready()
        {
            _fightData = GetNode<FightData>(AutoLoadPaths.FIGHT_DATA_PATH);
            _resultsLabel = GetNode<Label>("%ResultsLabel");
            _sceneChangeLabel = GetNode<SceneChangeLabel>("%SceneChangeLabel");

            SetResultsLabelText();
            _fightData.Reset();

            _sceneChangeLabel.Visible = true; // TODO: Move this to after sound plays
        }

        private void SetResultsLabelText()
        {
            _resultsLabel.Text = _fightData.PlayerHealth == 0 ? _loseText : _winText;
        }
    }
}