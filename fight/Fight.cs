using LudumDare51.Actors;
using LudumDare51.AutoLoad;
using LudumDare51.UserInterface;
using Godot;
using System;

namespace LudumDare51
{
    public class Fight : Node
    {
        private const string INTERMISSION_PATH = "res://intermission/intermission.tscn";

        private FightData _fightData;

        Player player;
        Enemy enemy;

        private Timer _fightTimer;
        private Timer _fightEndDelay;

        public override void _Ready()
        {
            _fightData = GetNode<FightData>(AutoLoadPaths.FIGHT_DATA_PATH);
            player = GetNode<Player>("%Player");
            enemy = GetNode<Enemy>("%Enemy");
            _fightEndDelay = GetNode<Timer>("%FightEndDelay");
            _fightTimer = GetNode<Timer>("%FightTimer");

            _fightData.Round++;

            GetNode<FightDisplay>("%FightDisplay").Initialize(player, enemy, _fightTimer);

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
            if (player.Health == 0 || enemy.Health == 0)
            {
                _fightData.Reset();
            }

            GetTree().Paused = false;
            GetTree().ChangeScene(INTERMISSION_PATH);
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