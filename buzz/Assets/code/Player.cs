using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private bool _isFacingRight;
    private CharacterController2D _controller;
    private float _normalizedHorizontalSpeed;

    public float MaxSpeed = 8f;
    public float SpeedAccelerationOnGround = 10f;
    public float SpeedAccelerationInAir = 5f;


    public void Start()
    {
        _controller = GetComponent<CharacterController2D>();

        // facing right if my sprite is not flipped
        _isFacingRight = transform.localScale.x > 0;
    }


    // Called every frame
    public void Update()
    {
        HandleInput();

        var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
        
        _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));

    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _normalizedHorizontalSpeed = 1;
            if (!_isFacingRight)
            {
                Flip();
            }
        } else if (Input.GetKey(KeyCode.A))
        {
            _normalizedHorizontalSpeed = -1;
            if (_isFacingRight)
            {
                Flip();
            }
        } else
        {
            _normalizedHorizontalSpeed = 0;
        }



        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (_controller.CanJump)
                _controller.Jump();
            else
                Debug.Log("Can't jump... " + _controller.State);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.SetPositionAndRotation(new Vector3(5, 6, 0), new Quaternion());
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;
    }
}
