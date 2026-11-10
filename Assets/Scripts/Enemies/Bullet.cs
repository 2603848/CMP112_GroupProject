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

    protected virtual void Move()
    {
        Vector3 moveVec = new Vector3(0, 0, 0);
        if (Target != null)
        {
            moveVec = (Target.transform.position - this.transform.position).normalized;
        }
        else
        {
            moveVec = TargetDirection.normalized;
        }

        this.transform.position += moveVec * Speed * Time.deltaTime;
    }
    void OnCollisionEnter(Collision collision)
    {
        // if we are colliding with an object other than the one spawning it
        GameObject other = collision.gameObject;
        if (other.GetEntityId() != SpawnedBy)
        {
            IHasHealth otherHealth = other.GetComponent<IHasHealth>();
            if (otherHealth != null)
            {
                otherHealth.Damage(damage);
            }
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
