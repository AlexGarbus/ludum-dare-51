using Godot;
using LudumDare51.Actors;
using LudumDare51.AutoLoad;

namespace LudumDare51.UserInterface
{
    public class FightDisplay : Control
    {
        private FightData _fightData;

        private FightTimer _fightTimer;

        private HealthBar _playerHealthBar;
        private HealthBar _enemyHealthBar;

        private Label _roundLabel;

        public override void _Ready()
        {
            _fightData = GetNode<FightData>(AutoLoadPaths.FIGHT_DATA_PATH);
            _fightTimer = GetNode<FightTimer>("%FightTimer");
            _playerHealthBar = GetNode<HealthBar>("%PlayerHealthBar");
            _enemyHealthBar = GetNode<HealthBar>("%EnemyHealthBar");
            _roundLabel = GetNode<Label>("%RoundLabel");
        }

        public void Initialize(Player player, Enemy enemy, Timer fightTimer)
        {
            _playerHealthBar.Initialize(player);
            _enemyHealthBar.Initialize(enemy);
            _fightTimer.Initialize(fightTimer);

            _roundLabel.Text = $"Round {_fightData.Round}";
        }
    }
}