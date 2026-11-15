using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    public Animator weapon;
    public Rigidbody player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player && weapon)
            weapon.SetFloat("Speed", player.linearVelocity.magnitude);
        if (Input.GetMouseButtonDown(0))
            JustFired();
    }
    void ResetFiringSwitch()
    {
        if (weapon)
            weapon.SetBool("Attacking", false);
    }
    void JustFired()
    {
        if (weapon)
            weapon.SetBool("Attacking", true);
    }
    public void ResetReloading()
    {
        if (animator)
            animator.SetBool("Reloading", false);
    }
}
