using LudumDare51.Actors;
using LudumDare51.AutoLoad;
using LudumDare51.UserInterface;
using Godot;
using System;

namespace LudumDare51.GameScenes
{
    public class Fight : Node
    {
        private const string INTERMISSION_PATH = "res://game_scenes/intermission/intermission.tscn";
        private const string RESULTS_PATH = "res://game_scenes/results/results.tscn";

        private FightData _fightData;

        private Player _player;
        private Enemy _enemy;

        private Timer _fightTimer;
        private Timer _fightEndDelay;

        public override void _Ready()
        {
            _fightData = GetNode<FightData>(AutoLoadPaths.FIGHT_DATA_PATH);
            _player = GetNode<Player>("%Player");
            _enemy = GetNode<Enemy>("%Enemy");
            _fightEndDelay = GetNode<Timer>("%FightEndDelay");
            _fightTimer = GetNode<Timer>("%FightTimer");

            _fightData.Round++;

            GetNode<FightDisplay>("%FightDisplay").Initialize(_player, _enemy, _fightTimer);

            GetTree().Paused = true;
        }

        private void StartFightEndDelay(bool paused)
        {
            GetTree().Paused = paused;
            _fightEndDelay.Start();
        }

        private void OnFightStartDelayTimeout()
        {
            GetTree().Paused = false;
        }

        private void OnFightTimerTimeout()
        {
            StartFightEndDelay(true);
        }

        private void OnFightEndDelayTimeout()
        {
            GetTree().Paused = false;

            if (_player.Health == 0 || _enemy.Health == 0)
            {
                GetTree().ChangeScene(RESULTS_PATH);
            }
            else
            {
                GetTree().ChangeScene(INTERMISSION_PATH);
            }
        }

        private void OnActorHealthChanged(int value)
        {
            if (value == 0)
            {
                _fightTimer.Stop();
                StartFightEndDelay(false);
            }
        }
    }
}