using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEnemy : Enemy
{

    public float visibleHeight = 0f;
    public float hiddenHeight = -2f;
    public float movementSpeed = 3f;
    public float waitingDuration = 5f;

    private bool _hiding = true;

    private float _waitingTimer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
        if (_hiding) {
            if (transform.localPosition.y > hiddenHeight) {
                transform.localPosition = new Vector3 (
                    transform.localPosition.x,
                    transform.localPosition.y - movementSpeed * Time.deltaTime,
                    transform.localPosition.z
                );
            } else {
                _waitingTimer -= Time.deltaTime;
                if (_waitingTimer <= 0f) {
                    _waitingTimer = waitingDuration;
                    _hiding = false;
                }
            }
        } else {
            if (transform.localPosition.y < visibleHeight) {
                transform.localPosition = new Vector3 (
                    transform.localPosition.x,
                    transform.localPosition.y + movementSpeed * Time.deltaTime,
                    transform.localPosition.z
                );
            } else {
                _waitingTimer -= Time.deltaTime;
                if (_waitingTimer <= 0f) {
                    _waitingTimer = waitingDuration;
                    _hiding = true;
                }
            }
        }
    }
}
