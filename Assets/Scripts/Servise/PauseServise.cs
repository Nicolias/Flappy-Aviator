using UnityEngine;

public class PauseServise : MonoBehaviour
{
    [SerializeField] private PauseObject _menuPanel, _shopWindow;
    [SerializeField] private AudioServise _audioServise;

    private bool _isPlaneSoundWasPlayed;

    public void Pause()
    {
        if (_audioServise.PlaneSound.isPlaying)
        {
            _audioServise.PlaneSound.Stop();
            _isPlaneSoundWasPlayed = true;
        }

        Time.timeScale = 0;
    }

    public void TryUnPause()
    {
        if (_menuPanel.gameObject.activeInHierarchy == false && _shopWindow.gameObject.activeInHierarchy == false)
        {
            if (_isPlaneSoundWasPlayed)
                _audioServise.PlaneSound.Play();

            Time.timeScale = 1;
        }
    }
}
