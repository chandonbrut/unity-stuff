﻿using UnityEngine;
using System.Collections;

public class PathedProjectileSpawner : MonoBehaviour
{

    public Transform Destination;
    public PathedProjectile Projectile;
    public GameObject SpawnEffect;
    public AudioClip SpawnSound;

    public Animator Animator;

    public float Speed;
    public float FireRate;

    private float _nextShotInSeconds;

    // Use this for initialization
    void Start()
    {
        _nextShotInSeconds = FireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if ((_nextShotInSeconds -= Time.deltaTime) > 0) return;

        _nextShotInSeconds = FireRate;
        var projectile = (PathedProjectile)Instantiate(Projectile, transform.position, transform.rotation);
        projectile.Initialize(Destination, Speed);

        if (Animator != null) Animator.SetTrigger("Fire");
        if (SpawnEffect != null) Instantiate(SpawnEffect, transform.position, transform.rotation);
        if (SpawnSound != null) AudioSource.PlayClipAtPoint(SpawnSound, transform.position);

    }

    public void OnDrawGizmos()
    {

        if (Destination == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Destination.position);
    }
}
