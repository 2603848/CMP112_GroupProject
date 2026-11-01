using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoElement : MonoBehaviour
{
    public Image AmmoTypeImage;
    public TextMeshProUGUI AmmoTextTag;

    static AmmoElement SingletonReference;

    // Would prefer for this to be a dict<Weapon.AmmoType, Sprite> but apparently that is not serialisable
    public List<AmmoSprite> AmmoTypeSprites;

    public static void UpdateAmmoType(Weapon.AmmoType ammoType)
    {
        AmmoSprite sprite = SingletonReference.AmmoTypeSprites.Find(
            w  => { return w.AmmoType == ammoType; 
            });
        SingletonReference.AmmoTypeImage.sprite = sprite.UIAmmoSprite;
    }
    public static void UpdateAmmo(int ammo, int maxAmmo)
    {
        SingletonReference.AmmoTextTag.text = ammo + "/" + maxAmmo;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SingletonReference = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
