using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animator && agent)
        {
            animator.SetFloat("Speed",agent.velocity.magnitude);
        }
    }
    public void Attacking()
    {
        Debug.Log("attacking");
        animator.SetBool("Attacking", true);
    }
    public void ResetAttackingAnim()
    {
        animator.SetBool("Attacking", false);
    }
    // Called from the animation as a function call on 
    public void ResetDamageAnim()
    {
        animator.SetBool("Damaged", false);
    }
    public void SetDamaged()
    {
        animator.SetBool("Damaged", true);
    }
}
