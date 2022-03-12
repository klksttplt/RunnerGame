using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float acceleration = 2.5f;
    public float movementSpeed = 4f;
    public float jumpingSpeed = 6f;
    public float jumpDuration = 0.75f;
    public float verticalWallJumpingSpeed = 5f;
    public float horizontalWallJumpingSpeed = 3.5f;
    public float startSpeed = 2f;
    
    private float _speed;
    private float _jumpingTimer = 0f;

    private bool _canJump = false;
    private bool _jumping = false;
    private bool _canWallJump = false;
    private bool _wallJumpLeft = false;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        _speed = startSpeed;
        
        //Accelerate the player 
        _speed += acceleration * Time.deltaTime;
        if (_speed > movementSpeed) _speed = movementSpeed;

        //Move horizontally
        GetComponent<Rigidbody>().velocity = new Vector3(
            _speed,
            GetComponent<Rigidbody>().velocity.y,
            GetComponent<Rigidbody>().velocity.z
        );

        //Check for input
        var pressingJumpButton = Input.GetMouseButton(0) || Input.GetKey("space");
        if (pressingJumpButton)
            if (_canJump)
                _jumping = true;

        //Make the player jump
        if (_jumping)
        {
            _jumpingTimer += Time.deltaTime;

            if (pressingJumpButton && _jumpingTimer < jumpDuration)
                GetComponent<Rigidbody>().velocity = new Vector3(
                    GetComponent<Rigidbody>().velocity.x,
                    jumpingSpeed,
                    GetComponent<Rigidbody>().velocity.z
                );
        }

        //Make the player wall jump
        if (_canWallJump)
        {
            _speed = 0;

            if (pressingJumpButton)
            {
                _canWallJump = false;

                _speed = _wallJumpLeft ? -horizontalWallJumpingSpeed : horizontalWallJumpingSpeed;

                GetComponent<Rigidbody>().velocity = new Vector3(
                    GetComponent<Rigidbody>().velocity.x,
                    verticalWallJumpingSpeed,
                    GetComponent<Rigidbody>().velocity.z
                );
            }
        }
    }

    private void OnTriggerStay(Collider otherCollider)
    {
        //Floor collider
        if (otherCollider.CompareTag("JumpingArea"))
        {
            _canJump = true;
            _jumping = false;
            _jumpingTimer = 0;
        }
        else if (otherCollider.CompareTag("WallJumpingArea"))
        {
            _canWallJump = true;
            _wallJumpLeft = transform.position.x < otherCollider.transform.position.x;
        }
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.CompareTag("WallJumpingArea"))
        {
            _canWallJump = false;
        }
    }
}