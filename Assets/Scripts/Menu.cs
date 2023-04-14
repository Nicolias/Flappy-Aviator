using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _shopButton, _confidiancly;

    [SerializeField] private GameObject _shop;

    private void OnEnable()
    {
        _shopButton.onClick.AddListener(() => _shop.SetActive(true));
        _confidiancly.onClick.AddListener(() => Application.OpenURL("https://pastebin.com/3f4XyiEn"));
    }

    private void OnDisable()
    {        
        _shopButton.onClick.RemoveAllListeners();
    }
}
