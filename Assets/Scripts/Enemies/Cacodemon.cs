using UnityEngine;

public class Cacodemon : Enemy
{
    TargetFinder targetFinder;
    public Weapon weapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetFinder = GetComponent<TargetFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon.CanFire() && targetFinder.Target != null && Vector3.Distance(targetFinder.Target.transform.position,transform.position) < Range)
        {
            // Fire weapon at the center of the player
            weapon.Fire(this.gameObject, null, targetFinder.Target.GetComponent<Collider>().bounds.center);
            // Set the animator state
            enemyAnimator.Attacking();
        }
    }
}
