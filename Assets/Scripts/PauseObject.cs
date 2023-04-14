using UnityEngine;

public class PauseObject : MonoBehaviour
{
    [SerializeField] private PauseServise _pauseServise;

    private void OnEnable()
    {
        _pauseServise.Pause();   
    }

    private void OnDisable()
    {
        _pauseServise.TryUnPause();
    }
}
