using UnityEngine;
using Zenject;

class Obstacle : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private int _boarder;

    private bool _canMove;

    private void Update()
    {
        if(_canMove)
            transform.position += Vector3.left * _speed * Time.deltaTime;

        if (transform.position.x < _boarder)
            gameObject.SetActive(false);
    }

    public void StopMovment()
    {
        _canMove = false;
    }

    public void Reset()
    {
        _canMove = true;
        gameObject.SetActive(false);
    }
}

