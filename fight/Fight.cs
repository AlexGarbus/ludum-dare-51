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

        public override void _Ready()
        {
            _fightData = GetNode<FightData>(AutoLoadPaths.FIGHT_DATA_PATH);
            Timer fightTimer = GetNode<Timer>("%FightTimer");
            Player player = GetNode<Player>("%Player");
            Enemy enemy = GetNode<Enemy>("%Enemy");

            _fightData.Round++;

            fightTimer.Connect("timeout", this, nameof(OnFightTimerTimeout));

            GetNode<FightDisplay>("%FightDisplay").Initialize(player, enemy, fightTimer);
        }

        private void OnFightTimerTimeout()
        {
            GetTree().ChangeScene(INTERMISSION_PATH);
        }
    }
}