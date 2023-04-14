using Bet_Window;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameStateMachine
{
    public class GameStateSwitcher : MonoBehaviour
    {
        public event Action OnGameStarted;
        public event Action OnGameEnded;

        [SerializeField] private BetPanel _betPanel;
        [SerializeField] private ObstaclesFactory _obstaclesFactory;
        [SerializeField] private LosePanel _losePanel;
        [SerializeField] private WinCounter _winCounter;
        [SerializeField] private CreditPanel _creditPanel;
        [SerializeField] private AudioServise _audioServise;

        internal GameBaseState CurrentState { get; private set; }
        private List<GameBaseState> _allStates;

        private Plane _plane;

        private int _currentBet;
        internal int CurrentBet => _currentBet;
        internal int CurrentScore => _winCounter.Score;
        internal int PointPerWinTrigger { get; private set; }

        [Inject]
        public void Construct(Plane plane)
        {
            _plane = plane;
        }

        private void Start()
        {
            _allStates = new()
            {
                new GameBetState(_betPanel, this),
                new GamePlayState(_plane, _audioServise, this),
                new GameLoseState(_obstaclesFactory, _losePanel, _creditPanel, this)
            };

            SwitchState<GameBetState>();
        }

        public void StartGame(int bet, int pointPerWinTrigger)
        {
            _currentBet = bet;
            PointPerWinTrigger = pointPerWinTrigger;
            SwitchState<GamePlayState>();
            OnGameStarted?.Invoke();
        }

        public void Lose()
        { 
            OnGameEnded?.Invoke();
            StartCoroutine(InvokeActionAfterSeconds(1f, () => { SwitchState<GameLoseState>(); }));
        }

        internal void SwitchState<T>() where T : GameBaseState
        {
            if(CurrentState != null)
                CurrentState.Exit();

            var state = _allStates.FirstOrDefault(s => s is T);
            state.Enter();
            CurrentState = state;
        }

        private IEnumerator InvokeActionAfterSeconds(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            action.Invoke();
        }
    }
}
