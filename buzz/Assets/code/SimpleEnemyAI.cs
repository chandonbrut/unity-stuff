using UnityEngine;
using System.Collections;

public class SimpleEnemyAI : MonoBehaviour, ITakeDamage, IPlayerRespawnListener
{

    public float Speed;
    public float FireRate = 1;
    public Projectile Projectile;
    public GameObject DestroyedEffect;
    public AudioClip KilledSound;

    private CharacterController2D _controller;
    private Vector2 _direction;
    private Vector2 _startPosition;
    private float _canFireIn;

    public void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player)
    {
        _direction = new Vector2(-1, 0);
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = _startPosition;
        gameObject.SetActive(true);
    }

    public void TakeDamage(int damage, GameObject instigator)
    {
        AudioSource.PlayClipAtPoint(KilledSound, transform.position);
        Instantiate(DestroyedEffect,transform.position,transform.rotation);
        Debug.Log("Destroying!");
        gameObject.SetActive(false);

        var awarder = gameObject.GetComponent<AwardPoints>();
        if (awarder != null) awarder.AwardPointsCo();

    }

    // Use this for initialization
    void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _direction = new Vector2(-1, 0);
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _controller.SetHorizontalForce(_direction.x * Speed);

        var shouldFlip = (_direction.x < 0 && _controller.State.IsCollidingLeft) || (_direction.x > 0 && _controller.State.IsCollidingRight);

        if (shouldFlip)
        {
            _direction = -_direction;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if ((_canFireIn -= Time.deltaTime) > 0)
        {
            return;
        }

        var rayCast = Physics2D.Raycast(transform.position, _direction, 10, 1 << LayerMask.NameToLayer("Player"));

        if (!rayCast) return;

        var projectile = (Projectile)Instantiate(Projectile, transform.position, transform.rotation);
        projectile.Initialize(gameObject, _direction, _controller.Velocity);
        _canFireIn = FireRate;

    }
}
