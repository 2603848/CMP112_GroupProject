using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

// NOTE: base class, to be inherited from
[RequireComponent(typeof(TargetFinder), typeof(CharacterController))]
abstract public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        IDLE,
        AGGROED
    }

    public EnemyState State { get; protected set; } = EnemyState.IDLE;
    public float Health { get; protected set; } = 100;
    public float Movespeed = 5.0f;
    public float Range = 10.0f;

    public void Damage(float dmg)
    {
        Health -= dmg;
        if (Health <= 0)
        {
            GameObject.Destroy(this);
        } 
    }

}
