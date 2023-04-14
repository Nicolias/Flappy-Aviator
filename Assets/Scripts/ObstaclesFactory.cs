using GameStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

class ObstaclesFactory : MonoBehaviour
{
    [SerializeField] private List<Obstacle> _obstaclesVariation;
    [SerializeField] private float _spawnFrequency;
    [SerializeField] private Vector2 _spawnPosition;

    private DiContainer _di;

    private Queue<Obstacle> _obstaclesPool = new();
    private GameStateSwitcher _gameStateSwitcher;

    [Inject]
    public void Construct(GameStateSwitcher gameStateSwitcher, DiContainer di)
    {
        _gameStateSwitcher = gameStateSwitcher;
        _di = di;
    }

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            var obstacle = _di.InstantiatePrefabForComponent<Obstacle>(_obstaclesVariation[Random.Range(0, _obstaclesVariation.Count)], transform);
            obstacle.gameObject.SetActive(false);
            _obstaclesPool.Enqueue(obstacle);
        }  
    }

    private void OnEnable()
    {
        _gameStateSwitcher.OnGameStarted += StartSpawnObstacles;
        _gameStateSwitcher.OnGameEnded += EndSpawnObstacles;
    }

    private void OnDisable()
    {
        _gameStateSwitcher.OnGameStarted -= StartSpawnObstacles;
        _gameStateSwitcher.OnGameEnded -= EndSpawnObstacles;
    }

    public void TurnOffAllObstecle()
    {
        foreach (var obstacle in _obstaclesPool)
        {
            obstacle.gameObject.SetActive(false);
        }
    }

    private void StartSpawnObstacles()
    {
        StartCoroutine(SpawnObstacles());

        foreach (var obstacle in _obstaclesPool)
            obstacle.Reset();
    }

    private void EndSpawnObstacles()
    {
        StopAllCoroutines();

        foreach (var obstacle in _obstaclesPool)
            obstacle.StopMovment();
    }

    private IEnumerator SpawnObstacles()
    {
        if (_spawnFrequency <= 0) throw new System.Exception();

        while (true)
        {
            yield return new WaitForSeconds(_spawnFrequency);

            var currentObstacle = _obstaclesPool.Dequeue();

            currentObstacle.transform.position = _spawnPosition;
            currentObstacle.gameObject.SetActive(true);
            _obstaclesPool.Enqueue(currentObstacle);
        }
    }
}