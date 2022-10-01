using LudumDare51.Actors;
using LudumDare51.UserInterface;
using Godot;
using System;

namespace LudumDare51
{
    public class Fight : Node
    {
        public override void _Ready()
        {
            Player player = GetNode<Player>("%Player");
            Enemy enemy = GetNode<Enemy>("%Enemy");
            GetNode<FightDisplay>("%FightDisplay").Initialize(player, enemy);
        }
    }
}