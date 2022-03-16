using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{
    public GameObject model;
    public float speed = 3f;
    public float movementAmplitude = 4f;

    private Vector3 _initialPosition;
    private bool _movingLeft = true;

    // Use this for initialization
    protected virtual void Start()
    {
        _initialPosition = transform.position;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position = new Vector3(
            transform.position.x + speed * Time.deltaTime * (_movingLeft ? -1 : 1),
            transform.position.y,
            transform.position.z
        );

        if (_movingLeft == true && transform.position.x < _initialPosition.x - movementAmplitude / 2)
            _movingLeft = false;
        else if (_movingLeft == false && transform.position.x > _initialPosition.x + movementAmplitude / 2)
            _movingLeft = true;
    }
}