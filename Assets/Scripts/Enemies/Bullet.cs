using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoundsChecker))]
public class Bullet : MonoBehaviour
{
    public EntityId SpawnedBy;
    public GameObject Target = null;
    public Vector3 TargetDirection = new Vector3(0,0,0);

    public float Speed = 5.0f;
    public float damage = 12.0f;

    public BulletAnimator animator;

    Rigidbody rigidBody;

    public void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    protected virtual void Move()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        // if we are colliding with an object other than the one spawning it
        GameObject other = collision.gameObject;
        if (other.GetEntityId() != SpawnedBy)
        {
            // Are we targeting something that can be damaged?
            IHasHealth otherHealth = other.GetComponent<IHasHealth>();
            if (otherHealth != null)
            {
                otherHealth.Damage(damage);
            }
            // Destroy the bullet after it's finished playing its destruction animation
            float time = 0;
            if (animator != null)
                time = animator.GetDestroyAnimationTime();
            Destroy(this.gameObject, time);
            animator.DoDestroyAnimation();

            // Destroy the collider immediately so that we can't re-trigger the damage
            Destroy(collision.thisCollider);

        }
    }
    void FixedUpdate()
    {
        Move();
    }
}
