using UnityEngine;
using Zenject;

public class PointsCounterTrigger : MonoBehaviour
{
    private PointsCounter _winCounter;

    [Inject]
    public void Construct(PointsCounter winCounter)
    {
        _winCounter = winCounter;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _winCounter.AddScore();
    }
}
