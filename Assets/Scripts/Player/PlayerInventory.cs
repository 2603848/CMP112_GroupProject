using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    List<Weapon> inventory = new List<Weapon>();
    Weapon equippedWeapon;
    public Camera camera;

    public WeaponAnimator ViewModel;

    bool Reloading = false;

    void Equip()
    {

    }
    void ReloadWeapon()
    {
        equippedWeapon.ReloadWeapon();
        Gui.UpdateAmmo(equippedWeapon.Ammo, equippedWeapon.MaxAmmo);
    }
    void Reload()
    {
        foreach (AnimationClip clip in ViewModel.animator.runtimeAnimatorController.animationClips)
        {
            // If we've found the correct clip, set the animator to reload
            if (clip.name == "Reload")
            {
                ViewModel.animator.SetBool("Reloading", true);
                // Add the ammo only when the animation finishes
                Invoke(nameof(ReloadWeapon), clip.length);
            }
        }
    }

    bool IsReloading()
    {
        return ViewModel.animator.GetBool("Reloading");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject g = gameObject.transform.GetChild(i).gameObject;
            Weapon weapon = g.GetComponent<Weapon>();

            if (weapon != null)
                inventory.Add(weapon);
        }
        if (inventory.Count > 0)
            equippedWeapon = inventory[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Paused == false)
        {
            if (Input.GetKey(KeyCode.R) && !IsReloading())
            {
                Reload();
            }
            if (Input.GetMouseButtonDown(0) && !IsReloading())
            {
                if (equippedWeapon != null)
                {
                    if (equippedWeapon.CanFire())
                    {
                        equippedWeapon.Fire(this.gameObject, null, camera.transform.position, camera.transform.position + camera.transform.forward);
                        ViewModel.IsAttacking();
                        Gui.UpdateAmmo(equippedWeapon.Ammo, equippedWeapon.MaxAmmo);
                    }
                }
            }
        }
    }
}
