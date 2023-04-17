namespace GameStateMachine
{
    internal class GamePlayState : GameBaseState
    {
        private Plane _plane;
        private AudioServise _audioServise;

        internal GamePlayState(Plane plane, 
            AudioServise audioServise,
            GameStateSwitcher gameStateSwitcher) 
            : base(gameStateSwitcher)
        {
            _audioServise = audioServise;
            _plane = plane;
        }

        public override void Enter()
        {
            _audioServise.PlaneSound.Play();

            _plane.gameObject.SetActive(true);
            _plane.OnDead += InvokeLoseGame;
        }

        public override void Exit()
        { 
            _plane.OnDead -= InvokeLoseGame;
            _plane.gameObject.SetActive(false);
        }

        private void InvokeLoseGame()
        {
            _audioServise.PlaneSound.Stop();            
            _audioServise.Dead.Play();

            GameStateSwitcher.Lose();
        }
    }
}
