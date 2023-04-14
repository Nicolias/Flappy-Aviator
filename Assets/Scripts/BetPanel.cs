using GameStateMachine;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Bet_Window
{
    public class BetPanel : MonoBehaviour
    {
        [SerializeField] private CreditPanel _creditPanel;

        [SerializeField] private Button _betButton;

        [SerializeField] private TMP_Text _betText;

        [SerializeField] private List<BetButton> _variationBets;

        private int _bet;
        private int _pointPerWinTrigger;

        private StaticData _staticData;
        private GameStateSwitcher _gameStateSwitcher;

        [Inject]
        public void Construct(StaticData staticData, GameStateSwitcher gameStateSwitcher)
        {
            _staticData = staticData;
            _gameStateSwitcher = gameStateSwitcher;
        }

        private void OnEnable()
        {
            foreach (var bet in _variationBets)
            {
                bet.Button.image.sprite = _staticData.BetRedFrame;
                bet.Button.onClick.AddListener(() => SelectBet(bet));
            }

            _bet = 0;

            _betText.text = "0";

            _betButton.onClick.AddListener(StartGame);
        }

        private void OnDisable()
        {
            foreach (var bet in _variationBets)
                bet.Button.onClick.RemoveAllListeners();

            _betButton.onClick.RemoveAllListeners();
        }

        private void SelectBet(BetButton betButton)
        {
            ResetFoneAllBetButtons();

            betButton.Button.image.sprite = _staticData.BetRedFone;

            _bet = betButton.Bet;
            _pointPerWinTrigger = betButton.PointPerWinTrigger;

            _betText.text = betButton.Bet.ToString();
        }

        private void ResetFoneAllBetButtons()
        {
            foreach (var bet in _variationBets)
                bet.Button.image.sprite = _staticData.BetRedFrame;
        }

        private void StartGame()
        {
            if (_bet == 0 || _creditPanel.CreditsCount < _bet) return;

            _gameStateSwitcher.StartGame(_bet, _pointPerWinTrigger);
        }
    }
}