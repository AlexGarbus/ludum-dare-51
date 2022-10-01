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

        private Timer _fightEndDelay;

        public override void _Ready()
        {
            _fightData = GetNode<FightData>(AutoLoadPaths.FIGHT_DATA_PATH);
            _fightEndDelay = GetNode<Timer>("%FightEndDelay");
            Timer fightTimer = GetNode<Timer>("%FightTimer");
            Player player = GetNode<Player>("%Player");
            Enemy enemy = GetNode<Enemy>("%Enemy");

            _fightData.Round++;

            GetNode<FightDisplay>("%FightDisplay").Initialize(player, enemy, fightTimer);

            GetTree().Paused = true;
        }

        private void OnFightStartDelayTimeout()
        {
            GetTree().Paused = false;
        }

        private void OnFightTimerTimeout()
        {
            GetTree().Paused = true;
            _fightEndDelay.Start();
        }

        private void OnFightEndDelayTimeout()
        {
            GetTree().Paused = false;
            GetTree().ChangeScene(INTERMISSION_PATH);
        }
    }
}