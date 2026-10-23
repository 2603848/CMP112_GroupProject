using UnityEngine;

public class Cacodemon : Enemy
{
<<<<<<< Updated upstream

=======
    TargetFinder targetFinder;
    public Weapon weapon;
>>>>>>> Stashed changes
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetFinder = GetComponent<TargetFinder>();
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        
=======
        if (weapon.CanFire() && targetFinder.Target != null && Vector3.Distance(targetFinder.Target.transform.position,transform.position) < Range)
        {
            weapon.Fire(this.gameObject, null, this.transform.position, targetFinder.Target.transform.position);
            enemyAnimator.Attacking();
        }
>>>>>>> Stashed changes
    }
}
