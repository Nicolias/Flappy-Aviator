
using Bet_Window;

namespace GameStateMachine
{
    internal class GameBetState : GameBaseState
    {
        private BetPanel _betPanel;

        internal GameBetState(BetPanel betPanel, GameStateSwitcher gameStateSwitcher)
            : base(gameStateSwitcher)
        {
            _betPanel = betPanel;
        }

        public override void Enter()
        {
            _betPanel.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            _betPanel.gameObject.SetActive(false);
        }
    }
}
