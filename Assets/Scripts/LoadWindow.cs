using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWindow : MonoBehaviour
{
    [SerializeField] private float _loadTime;
    [SerializeField] private AudioServise _audioServise;

    [SerializeField] private float _moveSpeed, _ampletude, _frequence;

    [SerializeField] private Transform _startPosition, _endPosition;
    [SerializeField] private GameObject _plane, _linePrefab;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _spawnLineFrequence;

    [SerializeField] private float _upEngel, _downEngel;

    private float _currentTime, _offset, _previesOffset;
    private float _nextSpawnLineTime;

    private List<GameObject> _spawnedLineCollection = new();

    private void Start()
    {
        _plane.transform.position = _startPosition.position;

        StartCoroutine(WaitUntilLoadScene());
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        _offset = -(_ampletude * Mathf.Sin(_currentTime * _frequence));
        Quaternion direction = _offset > _previesOffset ? Quaternion.Euler(0, 0, _upEngel) : Quaternion.Euler(0, 0, _downEngel);

        if (Math.Round(_offset, 2) == Math.Round(_previesOffset, 2) && Mathf.Abs(_offset) > Mathf.Abs(_previesOffset))
            direction = Quaternion.Euler(0, 0, 0);

        _plane.transform.position = new Vector3(_plane.transform.position.x + _moveSpeed * Time.deltaTime, _startPosition.position.y + _offset);
        _plane.transform.rotation = Quaternion.Slerp(_plane.transform.rotation, direction, _rotationSpeed * Time.deltaTime);

        _previesOffset = _offset;

        DrowLine(direction);
    }

    private void DrowLine(Quaternion direction)
    {
        if (Time.time >= _nextSpawnLineTime)
        {
            var line = Instantiate(_linePrefab);
            line.transform.position = _plane.transform.position;
            line.transform.rotation = Quaternion.Slerp(_plane.transform.rotation, direction, _rotationSpeed * Time.deltaTime);
            _spawnedLineCollection.Add(line);

            _nextSpawnLineTime = Time.time + _spawnLineFrequence;
        }
    }

    private IEnumerator WaitUntilLoadScene()
    {
        while (_plane.transform.position.x < _endPosition.position.x)
        {
            yield return new WaitForSeconds(0.1f);
        }

        foreach (var item in _spawnedLineCollection)
            Destroy(item.gameObject);

        gameObject.SetActive(false);
        _audioServise.BackGroundSound.Play();
    }
}
