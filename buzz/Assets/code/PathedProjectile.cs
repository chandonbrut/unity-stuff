using UnityEngine;
using System.Collections;

public class PathedProjectile : MonoBehaviour, ITakeDamage
{
    public int PointsToGiveToPlayer;

    public GameObject DestroyEffect;

    private Transform _destination;
    private float _speed;

    public void Initialize(Transform destination, float speed)
    {
        _destination = destination;
        _speed = speed;
    }

    public void TakeDamage(int damage, GameObject instigator)
    {
        Blowup();
        var projectile = instigator.GetComponent<Projectile>();
        if (projectile != null && projectile.Owner.GetComponent<Player>() != null && PointsToGiveToPlayer != 0)
        {
            GameManager.Instance.AddPoints(PointsToGiveToPlayer);
            FloatingText.Show(string.Format("+{0}", PointsToGiveToPlayer), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 50f));

        }

    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _destination.position, Time.deltaTime * _speed);
        var distanceSquared = (_destination.transform.position - transform.position).sqrMagnitude;

        if (distanceSquared > .01f * 0.1f) return;

        Blowup();
    }

    void Blowup()
    {
        if (DestroyEffect != null) Instantiate(DestroyEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
