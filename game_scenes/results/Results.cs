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

        private AudioStreamPlayer _winSound;
        private AudioStreamPlayer _loseSound;

        private FightData _fightData;

        private Label _resultsLabel;
        private SceneChangeLabel _sceneChangeLabel;

        public override void _Ready()
        {
            _winSound = GetNode<AudioStreamPlayer>("%WinSound");
            _loseSound = GetNode<AudioStreamPlayer>("%LoseSound");
            _fightData = GetNode<FightData>(AutoLoadPaths.FIGHT_DATA_PATH);
            _resultsLabel = GetNode<Label>("%ResultsLabel");
            _sceneChangeLabel = GetNode<SceneChangeLabel>("%SceneChangeLabel");

            SetResults();
            SaveBestScore();
            _fightData.Reset();
        }

        private void SetResults()
        {
            if (_fightData.PlayerHealth == 0)
            {
                _resultsLabel.Text = _loseText;
                _loseSound.Play();
            }
            else
            {
                _resultsLabel.Text = _winText;
                _winSound.Play();
            }
        }

        private void SaveBestScore()
        {
            if (_fightData.Round < SaveManager.CurrentSave.BestScore)
            {
                SaveManager.CurrentSave.BestScore = _fightData.Round;
                SaveManager.Save();
            }
        }

        private void OnSoundFinished()
        {
            _sceneChangeLabel.Visible = true;
        }
    }
}