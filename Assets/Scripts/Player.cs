using System;
using System.Collections;
using System.Diagnostics;
using System.Linq.Expressions;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
    [Header("Visuals")] public GameObject model;
    
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

    [Header("Powerups")] public float invincibilityDuration = 5f;

    public Action onCollectCoin;

    //private int _jumpCount = 0;

    private float _speed;
    private float _jumpingTimer = 0f;
    private float _jumpingSpeed;

    private bool _dead = false;
    private bool _canJump = false;
    private bool _jumping = false;
    private bool _canWallJump = false;
    private bool _wallJumpLeft = false;
    private bool _pause = false;
    private bool _onSpeedAreaLeft = false;
    private bool _onSpeedAreaRight = false;
    private bool _onLongJump = false;

    private bool _hasPowerUp = false;
    private bool _hasInvincibility = false;

    public bool Dead => _dead;

    // Start is called before the first frame update
    private void Start()
    {
        _speed = startSpeed;
        _jumpingSpeed = normalJumpingSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_dead) return;

        //Accelerate the player 
        _speed += acceleration * Time.deltaTime;

        //Assigning target speed
        var targetSpeed = movementSpeed;
        //Speed ups
        if (_onSpeedAreaLeft)
            targetSpeed = movementSpeedLeft;

        else if (_onSpeedAreaRight) targetSpeed = movementSpeedRight;

        //Deacceleration
        if (_speed > targetSpeed) _speed -= deacceleration * Time.deltaTime;

        //Move horizontally
        GetComponent<Rigidbody>().velocity = new Vector3(
            _pause ? 0 : _speed,
            GetComponent<Rigidbody>().velocity.y,
            GetComponent<Rigidbody>().velocity.z
        );

        //Check for input
        var pressingJumpButton = Input.GetMouseButtonDown(0) || Input.GetKeyDown("space");

        //Player can jump
        if (pressingJumpButton)
            if (_canJump)
                Jump();


        //Checking if player is paused
        if (_pause && pressingJumpButton) _pause = false;

        //Make the player jump
        if (_jumping)
        {
            _jumpingTimer += Time.deltaTime;

            if (pressingJumpButton && _jumpingTimer < jumpDuration)
            {
                if (_onLongJump) _jumpingSpeed = longJumpingSpeed;

                GetComponent<Rigidbody>().velocity = new Vector3(
                    GetComponent<Rigidbody>().velocity.x,
                    _jumpingSpeed,
                    GetComponent<Rigidbody>().velocity.z
                );
            }
        }

        //Make the player wall jump
        if (_canWallJump)
            //_speed = 0;

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


    private void ApplyInvincibility()
    {
        _hasInvincibility = true;
        StartCoroutine(InvincibilityRoutine());
    }

    private IEnumerator InvincibilityRoutine()
    {
        //Slow blinks
        float initialWaitingTime = invincibilityDuration * 0.75f;
        int initialBlinks = 20;

        for (int i = 0; i < initialBlinks; i++)
        {
            model.SetActive(!model.activeSelf);
            yield return new WaitForSeconds(initialWaitingTime / initialBlinks);
        }

        //Fast blinks
        float finalWaitingTime = invincibilityDuration * 0.25f;
        int finalBlinks = 35;
        
        for (int i = 0; i < finalBlinks; i++)
        {
            model.SetActive(!model.activeSelf);
            yield return new WaitForSeconds(finalWaitingTime / finalBlinks);
        }
        
        model.SetActive(true);

        yield return new WaitForSeconds(invincibilityDuration);
        
        _hasInvincibility = false;
    }

    public void Jump(bool hitEnemy = false)
    {
        _jumping = true;

        if (hitEnemy)
            GetComponent<Rigidbody>().velocity = new Vector3(
                GetComponent<Rigidbody>().velocity.x,
                _jumpingSpeed,
                GetComponent<Rigidbody>().velocity.z
            );
    }

    public void Pause()
    {
        _pause = true;
    }


    private void Kill()
    {
        _dead = true;
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 400, -2));
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<BoxCollider>().enabled = false;
    }

    private void ApplyPowerUp()
    {
        _hasPowerUp = true;
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        //Collecting coins
        if (otherCollider.transform.GetComponent<Coin>() != null)
        {
            Destroy(otherCollider.gameObject);
            onCollectCoin();
        }

        //Speed up areas
        if (otherCollider.GetComponent<SpeedArea>() != null)
        {
            var speedArea = otherCollider.GetComponent<SpeedArea>();
            if (speedArea.direction == Direction.Left) _onSpeedAreaLeft = true;
            else if (speedArea.direction == Direction.Right) _onSpeedAreaRight = true;
        }

        //Long jump area
        if (otherCollider.CompareTag("LongJumpArea")) _onLongJump = true;

        //Player dies
        if (otherCollider.GetComponent<Enemy>())
        {
            var enemy = otherCollider.GetComponent<Enemy>();
            if (!_hasInvincibility && !enemy.Dead)
                if (!_hasPowerUp)
                {
                    Kill();
                }
                else
                {
                    _hasPowerUp = false;
                    ApplyInvincibility();
                }
        }

        //Collect the power up
        if (otherCollider.GetComponent<PowerUp>() != null)
        {
            var powerUp = otherCollider.GetComponent<PowerUp>();
            powerUp.Collect();
            ApplyPowerUp();
        }
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
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        //Leaving wall area
        if (otherCollider.CompareTag("WallJumpingArea")) _canWallJump = false;

        //Leaving speed up area
        if (otherCollider.GetComponent<SpeedArea>() != null)
        {
            var speedArea = otherCollider.GetComponent<SpeedArea>();
            if (speedArea.direction == Direction.Left) _onSpeedAreaLeft = false;
            else if (speedArea.direction == Direction.Right) _onSpeedAreaRight = false;
        }

        //Leaving long jump area
        if (otherCollider.CompareTag("LongJumpArea")) _onLongJump = false;
    }
}