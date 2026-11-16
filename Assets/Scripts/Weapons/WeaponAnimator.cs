using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    public Animator animator;
    public Rigidbody player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (animator && player)
            animator.SetFloat("Speed", player.linearVelocity.magnitude);
    }
    void ResetAttacking()
    {
        if (animator)
            animator.SetBool("Attacking", false);
    }
    public void IsAttacking()
    {
        if (animator)
            animator.SetBool("Attacking", true);
    }
    public void ResetReloading()
    {
        if (animator)
            animator.SetBool("Reloading", false);
    }
}