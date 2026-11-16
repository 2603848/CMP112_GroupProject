using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    List<Weapon> inventory = new List<Weapon>();
    Weapon equippedWeapon;
    public Camera camera;

    public WeaponAnimator ViewModel;
    
    [SerializeField]
    GameObject[] weapons;

    void HideViewmodels()
    {
        foreach (GameObject g in weapons)
        {
            Weapon w = g.GetComponent<Weapon>();
            w.Viewmodel.gameObject.SetActive(false);
        }
    }

    void Equip(int slot)
    {
        HideViewmodels();
        if (slot >= 0 && slot < weapons.Length)
        { 
            equippedWeapon = weapons[slot].GetComponent<Weapon>();  
            equippedWeapon.Equip();
            AmmoElement.UpdateAmmo(equippedWeapon.Ammo, equippedWeapon.MaxAmmo);
            AmmoElement.UpdateAmmoType(equippedWeapon.ammoType);

        }
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
        Equip(0);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject g = gameObject.transform.GetChild(i).gameObject;
            Weapon weapon = g.GetComponent<Weapon>();

            if (weapon != null)
                inventory.Add(weapon);
        }
    }
    // Returns the index of the number key being pressed, starting at index 0 for the 1 key
    int GetNumberKeyDown()
    {
        if (Input.GetKey(KeyCode.Alpha0))
            return 10;
        if (Input.GetKey(KeyCode.Alpha1))
            return 0;
        if (Input.GetKey(KeyCode.Alpha2))
            return 1;
        if (Input.GetKey(KeyCode.Alpha3))
            return 2;
        if (Input.GetKey(KeyCode.Alpha4))
            return 3;
        if (Input.GetKey(KeyCode.Alpha5))
            return 4;
        if (Input.GetKey(KeyCode.Alpha6))
            return 5;
        if (Input.GetKey(KeyCode.Alpha7))
            return 6;
        if (Input.GetKey(KeyCode.Alpha8))
            return 7;
        if (Input.GetKey(KeyCode.Alpha9))
            return 8;
        return -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Paused == false)
        {
            // If we have any number key pressed, switch weapon
            if (GetNumberKeyDown() != -1)
            {
                int key = GetNumberKeyDown();
                if (!IsReloading())
                    Equip(key);
            }
            if (Input.GetKey(KeyCode.R) && !IsReloading())
            {
                Reload();
            }
            if (Input.GetMouseButtonDown(0) && !IsReloading())
            {
                if (equippedWeapon != null)
                {
                    if (equippedWeapon.Ammo <= 0 && equippedWeapon.CanFire())
                    {
                        ReloadWeapon();
                    }
                    // Perform IsReloading check again, as in the case of an instant reloading (no animation), we need to immediately fire again
                    if (equippedWeapon.CanFire() && !IsReloading())
                    {
                        equippedWeapon.Fire(this.gameObject, null, camera.transform.position, camera.transform.position + camera.transform.forward);
                        equippedWeapon.Viewmodel.GetComponent<WeaponAnimator>().IsAttacking();
                        Gui.UpdateAmmo(equippedWeapon.Ammo, equippedWeapon.MaxAmmo);
                    }
                }
            }
        }
    }
}
