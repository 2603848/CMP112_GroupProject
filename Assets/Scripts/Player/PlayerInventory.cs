using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    List<Weapon> inventory = new List<Weapon>();
    Weapon equippedWeapon;
    public Camera camera;

    public WeaponAnimator ViewModel;


    void Equip()
    {

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
            if (Input.GetMouseButtonDown(0))
            {
                if (equippedWeapon != null)
                {
                    if (equippedWeapon.CanFire())
                    {
                        equippedWeapon.Fire(this.gameObject, null, camera.transform.position, camera.transform.position + camera.transform.forward);
                        ViewModel.IsAttacking();
                    }
                }
            }
        }
    }
}
