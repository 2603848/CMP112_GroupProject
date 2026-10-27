using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int Ammo = 10;
    public int MaxAmmo = 10;

    public float ReloadTime = 1;

    public float WeaponCD = 1.0f;
    private bool _CanFire = true;


    public Bullet BulletPrefab;

    public bool CanFire()
    {
        return _CanFire;
    }

    private void WeaponHasCooledDown()
    {
        _CanFire = true;
    }

    protected virtual void _Fire(GameObject invoker, GameObject target, Vector3 startPosition, Vector3 targetPosition)
    {
        if (CanFire())
        {
            Debug.Log("Target pos: " + targetPosition.ToString());

            Bullet spawned = GameObject.Instantiate(BulletPrefab);
            spawned.transform.position = startPosition;
            spawned.Target = target;
            spawned.TargetDirection = (targetPosition - startPosition).normalized;
            spawned.SpawnedBy = invoker.GetEntityId();
            _CanFire = false;

            this.Invoke(nameof(WeaponHasCooledDown), WeaponCD);
        }
    }
    protected virtual void _Reload()
    {
        Ammo = MaxAmmo;
    }

    public void Fire(GameObject invoker, GameObject target, Vector3 startPosition, Vector3 targetPosition)
    {
        if (Ammo > 0)
        { 
           _Fire(invoker, target, startPosition, targetPosition);
            Ammo--;
        }
    }
    protected void Reload()
    {
        this.Invoke(nameof(_Reload),ReloadTime);
    }


}
