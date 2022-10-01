using LudumDare51.Actors;
using LudumDare51.UserInterface;
using Godot;
using System;

namespace LudumDare51
{
    public class Fight : Node
    {
        private const int FIGHT_TIME = 10;

        private SceneTreeTimer _fightTimer;

        public override void _Ready()
        {
            Player player = GetNode<Player>("%Player");
            Enemy enemy = GetNode<Enemy>("%Enemy");

            _fightTimer = GetTree().CreateTimer(FIGHT_TIME, false);

            GetNode<FightDisplay>("%FightDisplay").Initialize(player, enemy, _fightTimer);
        }
    }
}