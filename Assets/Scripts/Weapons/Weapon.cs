using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Profiling.HierarchyFrameDataView;

public abstract class Weapon : MonoBehaviour
{
    public int Ammo = 10;
    public int MaxAmmo = 10;


    public float WeaponCD = 1.0f;
    private bool _CanFire = true;

    // only applicable to the player's weapons - could be better to implement PlayerWeapon as a class
    // but that seems like a bit too much abstraction?
    public GameObject Viewmodel;

    // Maybe a better way to do this is to implement the hitscan in the Start() function of a Bullet?
    public bool IsHitscan = false;
    public float HitscanMaxDist = 1000;
    public LayerMask HitLayers;
    public float HitscanDamage = 10;

    public Bullet BulletPrefab;

    public GameObject BulletSpawnPoint;

    public bool UsesAmmo = true;
    public AmmoType ammoType = AmmoType.SHELLS;

    public enum AmmoType
    {
        NONE,
        SHELLS,
        BULLETS,
    }
    public void Equip()
    {
        Viewmodel.SetActive(true);
    }
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
        if (CanFire() ) 
        {
            // If it is a hitscan weapon, immediately apply the damage
            if (IsHitscan == true)
            {
                RaycastHit hit;
                if (Physics.Raycast(startPosition, (targetPosition - startPosition).normalized, out hit, HitscanMaxDist, HitLayers))
                {
                    GameObject collided = hit.collider.transform.gameObject;
                    // If the object is something that can be damaged, apply the damage
                    IHasHealth health = collided.GetComponent<IHasHealth>();
                    if (health != null)
                    {
                        health.Damage(HitscanDamage, this.transform.parent.parent.gameObject);
                    }
                }
            }
            if (IsHitscan == false)
            {
                Bullet spawned = GameObject.Instantiate(BulletPrefab);
                
                spawned.transform.rotation = Quaternion.identity;
                spawned.transform.position = startPosition;
                
                // Set the speed of the bullet to move to the target's position
                spawned.TargetDirection = (targetPosition - startPosition).normalized;
                spawned.GetComponent<Rigidbody>().linearVelocity = spawned.TargetDirection * spawned.Speed;

                // Ignore the collision between the bullet and this object's collider
                Physics.IgnoreCollision(spawned.GetComponent<Collider>(), transform.parent.GetComponent<Collider>());
                
                spawned.Target = target;
                spawned.SpawnedBy = invoker;
            }
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
        // Don't perform action if paused
        if (GameController.Paused)
            return;

        if (Ammo > 0)
        {
            _Fire(invoker, target, startPosition, targetPosition);
            if (UsesAmmo)
                Ammo--;
        }

    }
    // Overload that uses the startPosition set inside this Weapon
    public void Fire(GameObject invoker, GameObject target, Vector3 targetPosition)
    {
        if (BulletSpawnPoint != null)
            Fire(invoker, target, BulletSpawnPoint.transform.position, targetPosition);
    }
    public void ReloadWeapon()
    {
        Ammo = MaxAmmo;
    }
}
