namespace GameStateMachine
{
    internal abstract class GameBaseState
    {
        protected GameStateSwitcher GameStateSwitcher { get; private set; }

        internal GameBaseState(GameStateSwitcher gameStateSwitcher)
        {
            GameStateSwitcher = gameStateSwitcher;
        }

        public abstract void Enter();
        public abstract void Exit();

    }
}
