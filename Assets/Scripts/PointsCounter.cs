using GameStateMachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameStateSwitcher _gameStateSwitcher;

    private int _score;
    public int Score => _score;

    private void OnEnable()
    {
        _gameStateSwitcher.OnGameStarted += ResetScore;
        ResetScore();
    }

    private void OnDisable()
    {
        _gameStateSwitcher.OnGameStarted -= ResetScore;
    }

    public void AddScore()
    {
        UpdateScore(_score + _gameStateSwitcher.CurrentPrizeSystem.GetCurrentPointsPerStage());
    }

    private void ResetScore()
    {
        UpdateScore(0);
    }

    private void UpdateScore(int newScore)
    {
        _score = newScore;
        _scoreText.text = _score.ToString();
    }
}
