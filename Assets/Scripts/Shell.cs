using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float movementSpeed = 10f;

    private bool _movingRight = true;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        transform.position = new Vector3(
            transform.position.x + movementSpeed * (_movingRight ? 1 : -1) * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("''");
            Destroy(other.gameObject);
        }
        else
        {
            if (transform.position.x < other.transform.position.x && _movingRight)
                _movingRight = false;
            else if (transform.position.x > other.transform.position.x && _movingRight) _movingRight = true;
        }
    }
}