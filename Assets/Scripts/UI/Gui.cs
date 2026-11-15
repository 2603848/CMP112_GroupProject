using Unity.VisualScripting;
using UnityEngine;

public class Gui : MonoBehaviour
{
    private Gui SingletonReference;
    public static void UpdateHealth(int healthPercent)
    {
        HealthElement.SetHealthDisplayed(healthPercent);
    }
    public static void UpdateAmmo(int ammo, int maxAmmo)
    {
        AmmoElement.UpdateAmmo(ammo, maxAmmo);
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
