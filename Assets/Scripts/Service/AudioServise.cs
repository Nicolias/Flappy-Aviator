using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioServise : MonoBehaviour
{
    [field: SerializeField] public AudioSource Dead { get; private set; }
    [field: SerializeField] public AudioSource PlaneSound { get; private set; }
    [field: SerializeField] public AudioSource BackGroundSound { get; private set; }

    [SerializeField] private StaticData _staticData;

    [SerializeField] private Button _soundButtonInMenu, _soundButtonInUpPanel;
    [SerializeField] private TMP_Text _soundStatusText;

    private List<AudioSource> _audioClips = new();

    public bool IsMute { get; private set; }

    private void Awake()
    {
        _audioClips.Add(Dead);
        _audioClips.Add(PlaneSound);
        _audioClips.Add(BackGroundSound);
    }

    private void OnEnable()
    {
        ValidateSoundUI();

        _soundButtonInMenu.onClick.AddListener(ChangeMuteStatus);
        _soundButtonInUpPanel.onClick.AddListener(ChangeMuteStatus);
    }

    private void OnDisable()
    {
        _soundButtonInMenu.onClick.RemoveAllListeners();
        _soundButtonInUpPanel.onClick.RemoveAllListeners();
    }

    public void ChangeMuteStatus()
    {
        IsMute = !IsMute;

        foreach (var audioClip in _audioClips)
            audioClip.mute = IsMute;

        ValidateSoundUI();
    }

    private void ValidateSoundUI()
    {
        _soundStatusText.text = IsMute ? "OFF" : "ON";
        _soundButtonInUpPanel.image.sprite = IsMute ? _staticData.SoundOff : _staticData.SoundOn;
    }

}
