using UnityEngine;
using System.Collections;

public class DamagePlayer : MonoBehaviour
{
    public float DamageToGive = 10;

    private Vector2 _lastPosition, _velocity;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        _velocity = (_lastPosition - (Vector2)transform.position) / Time.deltaTime;
        _lastPosition = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null) return;

        player.TakeDamage(DamageToGive);

        var controller = player.GetComponent<CharacterController2D>();

        var resultingVelocity = controller.Velocity + _velocity;

        // Reverting components to create knockback effect, using clamp to limit the intensity of the effect.
        var xComponent = -1 * Mathf.Sign(resultingVelocity.x) * Mathf.Clamp(Mathf.Abs(resultingVelocity.x) * 5, 10, 20);
        var yComponent = -1 * Mathf.Sign(resultingVelocity.y) * Mathf.Clamp(Mathf.Abs(resultingVelocity.y) * 2, 0, 15);

        controller.SetForce(new Vector2(xComponent, yComponent));
    }
}
