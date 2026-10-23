using UnityEngine;

public class Cacodemon : Enemy
{
    TargetFinder targetFinder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetFinder = GetComponent<TargetFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon.CanFire() && targetFinder.Target != null)
        {
            weapon.Fire(this.gameObject, null, this.transform.position, targetFinder.Target.transform.position);
        }
    }
}
