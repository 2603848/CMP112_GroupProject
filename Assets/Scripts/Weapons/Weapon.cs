using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int Ammo = 10;
    public int MaxAmmo = 10;

    public float ReloadTime = 1;

    public float WeaponCD = 1.0f;
    private bool _CanFire = true;

    // only applicable to the player's weapons - could be better to implement PlayerWeapon as a class
    // but that seems like a bit too much abstraction?
    public Sprite Viewmodel;

    // Maybe a better way to do this is to implement the hitscan in the Start() function of a Bullet?
    public bool IsHitscan = false;
    public float HitscanMaxDist = 1000;
    public LayerMask HitLayers;
    public float HitscanDamage = 10;

    public Bullet BulletPrefab;

    public enum AmmoType
    {
        NONE,
        SHELLS,
        BULLETS,
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
        if (CanFire())
        {
            if (IsHitscan == true)
            {
                RaycastHit hit;
                if (Physics.Raycast(startPosition, (targetPosition - startPosition).normalized, out hit, HitscanMaxDist, HitLayers))
                {
                    GameObject collided = hit.collider.transform.parent.gameObject;
                    Debug.Log(collided.name);
                    IHasHealth health = collided.GetComponent<IHasHealth>();
                    if (health != null)
                    {
                        health.Damage(HitscanDamage);
                    }
                }
            }
            if (IsHitscan == false)
            {
                Bullet spawned = GameObject.Instantiate(BulletPrefab);
                spawned.transform.position = startPosition;
                spawned.Target = target;
                spawned.TargetDirection = (targetPosition - startPosition).normalized;
                spawned.SpawnedBy = invoker.GetEntityId();
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
            Ammo--;
        }
    }
    protected void Reload()
    {
        this.Invoke(nameof(_Reload),ReloadTime);
    }


}
