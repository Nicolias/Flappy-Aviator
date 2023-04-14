namespace GameStateMachine
{
    internal class GameLoseState : GameBaseState
    {
        private ObstaclesFactory _obstaclesFactory;
        protected LosePanel _losePanel;
        private CreditPanel _creditPanel;

        internal GameLoseState(ObstaclesFactory obstaclesFactory, LosePanel losePanel, CreditPanel creditPanel, GameStateSwitcher gameStateSwitcher) 
            : base(gameStateSwitcher)
        {
            _obstaclesFactory = obstaclesFactory;
            _losePanel = losePanel;
            _creditPanel = creditPanel;
        }

        public override void Enter()
        {
            _losePanel.gameObject.SetActive(true);
            _losePanel.OnGameRestarted += InvokeBetState;
            CalculatePrize();
        }

        public override void Exit()
        {
            _losePanel.gameObject.SetActive(false);
            _losePanel.OnGameRestarted -= InvokeBetState;
        }

        private void InvokeBetState()
        {
            GameStateSwitcher.SwitchState<GameBetState>();
            _obstaclesFactory.TurnOffAllObstecle();
        }

        private void CalculatePrize()
        {
            if (GameStateSwitcher.CurrentScore > GameStateSwitcher.CurrentBet)
                Win();
            else
                Lose();
        }

        private void Win()
        {
            _creditPanel.AddCredits(GameStateSwitcher.CurrentScore);
        }

        private void Lose()
        {
            _creditPanel.DecreaseCredits(GameStateSwitcher.CurrentBet);
        }
    }
}
