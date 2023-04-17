using UnityEngine;

[System.Serializable]
public class PrizeSystem
{ 
    [SerializeField] private int _basePointsByStage;
    [SerializeField] private int _pointsNeedForGetBonus;
    [SerializeField] private int _bonusPoints;

    [SerializeField] private PointsCounter _pointsCounter;

    public int GetCurrentPointsPerStage()
    {
        int currentPoints = _pointsCounter.Score > _pointsNeedForGetBonus ? _bonusPoints : _basePointsByStage;

        return currentPoints;
    }
}
