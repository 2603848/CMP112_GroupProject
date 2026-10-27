using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoundsChecker))]
public class Bullet : MonoBehaviour
{
    public EntityId SpawnedBy;
    public GameObject Target = null;
    public Vector3 TargetDirection = new Vector3(0,0,0);

    public float Speed = 5.0f;


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
            Destroy(this.gameObject);
        }
    }
    void Update()
    {
        Move();
    }
}
