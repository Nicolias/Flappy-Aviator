using TMPro;
using UnityEngine;
using System;

public class CreditPanel : MonoBehaviour
{
    public event Action OnCreditChanged;

    [SerializeField] private TMP_Text _creditsPanelText;

    private int _creditsCount = 15000;
    public int CreditsCount => _creditsCount;

    private string _credits = "Credits";

    private void Awake()
    {
        LoadCoinsCount();
    }

    public void AddCredits(int creditsCount)
    {
        if (creditsCount < 0)
            throw new InvalidOperationException
                ("Начисляется отрицательное число");

        _creditsCount += creditsCount;

        SaveNewCoinsCount();
    }

    public void DecreaseCredits(int creditsCount)
    {
        if (_creditsCount - creditsCount < 0)
            throw new InvalidOperationException("Недостаточно средств");

        _creditsCount -= creditsCount;

        SaveNewCoinsCount();        
    }

    private void LoadCoinsCount()
    {
        if (PlayerPrefs.HasKey(_credits))
            _creditsCount = PlayerPrefs.GetInt(_credits);

        UpdateCreditsUIText();
    }

    private void SaveNewCoinsCount()
    {
        PlayerPrefs.SetInt(_credits, _creditsCount);

        UpdateCreditsUIText();
        OnCreditChanged?.Invoke();
    }

    private void UpdateCreditsUIText()
    {
        _creditsPanelText.text = _creditsCount.ToString();
    }
}