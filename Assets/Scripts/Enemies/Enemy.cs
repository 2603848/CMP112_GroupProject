using UnityEngine;
using UnityEngine.AI;

// NOTE: base class, to be inherited from
[RequireComponent(typeof(TargetFinder), typeof(CharacterController))]
abstract public class Enemy : MonoBehaviour, IHasHealth
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

    public SpriteRenderer spriteRenderer;
    public NavMeshAgent agent;
    public EnemyAnimator enemyAnimator;

    public void Damage(float dmg)
    {
        Health -= dmg;
        Debug.Log(Health);
        if (Health > 0 && enemyAnimator != null)
        {
            enemyAnimator.SetDamaged();
        }
        if (Health <= 0)
        {
            enemyAnimator.SetDead();

            // Remove the sprite renderer component (previously a child of this gameobject) so that we keep the body around and the animator still runs the death animation
            spriteRenderer.gameObject.transform.parent = null;
            // and then destroy this object 
            GameObject.Destroy(this.gameObject);
        } 
    }
}
