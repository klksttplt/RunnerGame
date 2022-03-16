using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : Enemy
{
    public float speed = 3f;
    public float movementAmplitude = 4f;

    private Vector3 _initialPosition;
    private bool _movingLeft = true;

    //Start is called before the first frame update
    private void Start()
    {
        _initialPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(
            transform.position.x + speed * Time.deltaTime * (_movingLeft ? -1 : 1),
            transform.position.y,
            transform.position.z
        );

        if (_movingLeft && transform.position.x < _initialPosition.x - movementAmplitude / 2)
            _movingLeft = false;
        else if (!_movingLeft && transform.position.x > _initialPosition.x + movementAmplitude / 2) _movingLeft = true;
    }
}