using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWindow : MonoBehaviour
{
    [SerializeField] private float _loadTime;
    [SerializeField] private AudioServise _audioServise;

    private void Start()
    {
        StartCoroutine(WaitUntilLoadScene());
    }

    private IEnumerator WaitUntilLoadScene()
    {
        yield return new WaitForSeconds(_loadTime);

        gameObject.SetActive(false);
        _audioServise.BackGroundSound.Play();
    }
}
