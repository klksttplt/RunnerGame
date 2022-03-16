﻿using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Acceleration")] public float acceleration = 2.5f;
    public float deacceleration = 5f;

    [Header("Movement speed")] public float startSpeed = 2f;
    public float movementSpeed = 4f;
    public float movementSpeedRight = 8f;
    public float movementSpeedLeft = 2f;

    [Header("Jumping")] public float normalJumpingSpeed = 6f;
    public float longJumpingSpeed = 10f;
    public float jumpDuration = 0.75f;
    public float verticalWallJumpingSpeed = 5f;
    public float horizontalWallJumpingSpeed = 3.5f;


    public Action onCollectCoin;

    private int _jumpCount = 0;

    private float _speed;
    private float _jumpingTimer = 0f;
    private float _jumpingSpeed;

    private bool _canJump = false;
    private bool _jumping = false;
    private bool _canWallJump = false;
    private bool _wallJumpLeft = false;
    private bool _pause = false;
    private bool _onSpeedAreaLeft = false;
    private bool _onSpeedAreaRight = false;
    private bool _onLongJump = false;

    // Start is called before the first frame update
    private void Start()
    {
        _speed = startSpeed;
        _jumpingSpeed = normalJumpingSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        //Accelerate the player 
        _speed += acceleration * Time.deltaTime;

        var targetSpeed = movementSpeed;
        if (_onSpeedAreaLeft)
            targetSpeed = movementSpeedLeft;

        else if (_onSpeedAreaRight) targetSpeed = movementSpeedRight;

        if (_speed > targetSpeed) _speed -= deacceleration * Time.deltaTime;

        //Move horizontally
        GetComponent<Rigidbody>().velocity = new Vector3(
            _pause ? 0 : _speed,
            GetComponent<Rigidbody>().velocity.y,
            GetComponent<Rigidbody>().velocity.z
        );

        //Check for input
        var pressingJumpButton = Input.GetMouseButtonDown(0) || Input.GetKeyDown("space");

        if (pressingJumpButton)
            if ( _canJump)
                _jumping = true;

        //Checking if player is paused
        if (_pause && pressingJumpButton) _pause = false;

        //Make the player jump
        if (_jumping)
        {
            
            _jumpingTimer += Time.deltaTime;

            if (pressingJumpButton && _jumpingTimer < jumpDuration)
            {
                if (_onLongJump)
                {
                    _jumpingSpeed = longJumpingSpeed;
                }

                GetComponent<Rigidbody>().velocity = new Vector3(
                    GetComponent<Rigidbody>().velocity.x,
                    _jumpingSpeed,
                    GetComponent<Rigidbody>().velocity.z
                );
                
            }
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

    public void Pause()
    {
        _pause = true;
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.transform.GetComponent<Coin>() != null)
        {
            Destroy(otherCollider.gameObject);
            onCollectCoin();
        }

        if (otherCollider.GetComponent<SpeedArea>() != null)
        {
            var speedArea = otherCollider.GetComponent<SpeedArea>();
            if (speedArea.direction == Direction.Left) _onSpeedAreaLeft = true;
            else if (speedArea.direction == Direction.Right) _onSpeedAreaRight = true;
        }

        if (otherCollider.GetComponent<LongJumpBlock>() != null) _onLongJump = true;
    }

    private void OnTriggerStay(Collider otherCollider)
    {
        //Floor collider
        if (otherCollider.CompareTag("JumpingArea"))
        {
            
            
            _canJump = true;
            _jumping = false;
            _jumpingSpeed = normalJumpingSpeed;
            _jumpingTimer = 0;
            
        }
        //Wall collider
        else if (otherCollider.CompareTag("WallJumpingArea"))
        {
            _canWallJump = true;
            _wallJumpLeft = transform.position.x < otherCollider.transform.position.x;
        }
        
        if (otherCollider.GetComponent<LongJumpBlock>() != null) _jumpingSpeed = longJumpingSpeed;
        
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        //Wall collider
        if (otherCollider.CompareTag("WallJumpingArea")) _canWallJump = false;

        if (otherCollider.GetComponent<SpeedArea>() != null)
        {
            var speedArea = otherCollider.GetComponent<SpeedArea>();
            if (speedArea.direction == Direction.Left) _onSpeedAreaLeft = false;
            else if (speedArea.direction == Direction.Right) _onSpeedAreaRight = false;
        }

        if (otherCollider.GetComponent<LongJumpBlock>() != null) _onLongJump = false;
    }
}