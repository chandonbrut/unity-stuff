using UnityEngine;
using UnityEditor;

public class SimpleProjectile : Projectile, ITakeDamage
{
    public int Damage;
    public GameObject DestroyedEffect;
    public float TimeToLive;

    private void DestroyProjectile()
    {
        if (DestroyedEffect != null) Instantiate(DestroyedEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    public void Update()
    {

        if ((TimeToLive -= Time.deltaTime) <= 0)
        {
            DestroyProjectile();
            return;
        }

        //transform.Translate((Direction + (new Vector2(InitialVelocity.x, 0) * Speed * Time.deltaTime)), Space.World);
        transform.Translate(Direction * ((Mathf.Abs(InitialVelocity.x) + Speed) * Time.deltaTime),Space.World);

    }

    protected override void OnCollideOther(Collider2D other)
    {
        DestroyProjectile();
    }

    protected override void OnCollideTakeDamage(Collider2D other, ITakeDamage takeDamage)
    {
        takeDamage.TakeDamage(Damage, gameObject);
        DestroyProjectile();
    }
    public void TakeDamage(int damage, GameObject instigator)
    {

        var projectile = instigator.GetComponent<Projectile>();

        if (projectile != null && projectile.Owner.GetComponent<Player>() != null)
        {
            var awarder = gameObject.GetComponent<AwardPoints>();
            if (awarder != null) awarder.AwardPointsCo();
        }

        DestroyProjectile();

    }
}