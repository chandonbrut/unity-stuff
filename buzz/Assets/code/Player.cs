using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour { 

    private bool _isFacingRight;
    private CharacterController2D _controller;
    private float _normalizedHorizontalSpeed;

    public float MaxSpeed = 8f;
    public float MaxHealth = 100;
    
    public float SpeedAccelerationOnGround = 10f;
    public float SpeedAccelerationInAir = 5f;

    public float Health { get; set; }
    public bool IsDead { get; private set; }

    public GameObject OuchEffect;
    public Transform ProjectileFireLocation;
    public Projectile Projectile;
    public float FireRate;

    public AudioClip PlayerHealSound;
    public AudioClip JumpSound;
    public AudioClip DeathSound;
    public AudioClip PlayerHitSound;
    public AudioClip PlayerShootSound;

    private float _canFireIn;

    public void Awake()
    {
        _controller = GetComponent<CharacterController2D>();
        Health = MaxHealth;

        // facing right if my sprite is not flipped
        _isFacingRight = transform.localScale.x > 0;
    }


    // Called every frame
    public void Update()
    {

        _canFireIn -= Time.deltaTime;

        if (!IsDead) HandleInput();

        var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
        
        _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));

    }

    public void GiveHealth(float health, GameObject giver)
    {

        AudioSource.PlayClipAtPoint(PlayerHealSound, transform.position);
        Health =  Mathf.Min(MaxHealth,health+Health);

        FloatingText.Show(string.Format("+{0}", health), "HealthText", new FromWorldPointTextPositioner(Camera.main, (transform.position + new Vector3(0, 4, 0)), 2f, 50f));

    }

    public void TakeDamage(float damage)
    {

        AudioSource.PlayClipAtPoint(PlayerHitSound, transform.position);
        Instantiate(OuchEffect, transform.position, transform.rotation);
        Health -= damage;

        FloatingText.Show(string.Format("-{0}", damage), "DamageText", new FromWorldPointTextPositioner(Camera.main, (transform.position+new Vector3(0,4,0)), 2f, 50f));


        if (Health <= 0) LevelManager.Instance.KillPlayer();
    }

    public void FinishLevel()
    {

        enabled = false;
        _controller.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    public void Kill()
    {
        AudioSource.PlayClipAtPoint(DeathSound, transform.position);
        _controller.HandleCollisions = false;
        GetComponent<Collider2D>().enabled = false;

        _controller.SetHorizontalForce(0);
        _normalizedHorizontalSpeed = 0;
        _controller.SetVerticalForce(_controller.Parameters.Gravity * -0.4f);
        IsDead = true;
        Health = 0f;
    }

    public void RespawnAt(Transform spawnPoint)
    {
        if (!_isFacingRight) Flip();

        IsDead = false;
        _controller.HandleCollisions = true;
        GetComponent<Collider2D>().enabled = true;
        transform.position = spawnPoint.position;
        Health = MaxHealth;
    }

    private void Jump()
    {

        if (_controller.CanJump)
        {
            AudioSource.PlayClipAtPoint(JumpSound, transform.position);
            _controller.Jump();
        }
        else
            Debug.Log("Can't jump... " + _controller.State);

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
            Jump();
            
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            LevelManager.Instance.Restart();
        }

        if (Input.GetMouseButtonDown(0))
        {
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        if (_canFireIn > 0) return;

        var direction = _isFacingRight ? Vector2.right : Vector2.left;
        AudioSource.PlayClipAtPoint(PlayerShootSound, transform.position);

        var projectile = (Projectile)Instantiate(Projectile, ProjectileFireLocation.position, ProjectileFireLocation.rotation);
        projectile.Initialize(gameObject, direction, _controller.Velocity);

        _canFireIn = FireRate;
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;
    }

    /*
    public void TakeDamage(int damage, GameObject instigator)
    {
        TakeDamage(damage * 1.0f);
    }
    */
}
