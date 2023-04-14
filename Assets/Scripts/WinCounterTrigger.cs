using UnityEngine;
using Zenject;

public class WinCounterTrigger : MonoBehaviour
{
    private WinCounter _winCounter;

    [Inject]
    public void Construct(WinCounter winCounter)
    {
        _winCounter = winCounter;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _winCounter.AddScore();
    }
}
