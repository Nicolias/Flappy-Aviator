using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bet_Window
{
    [RequireComponent(typeof(Button))]
    internal class BetButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _betText;

        [SerializeField] private int _bet;
        [SerializeField] private int _pointPerWinTrigger;

        public int Bet => _bet;
        public int PointPerWinTrigger => _pointPerWinTrigger;
        [field: SerializeField] public Button Button { get; private set; }

        private void OnEnable()
        {
            _betText.text = $"> {_bet}";
        }
    }
}