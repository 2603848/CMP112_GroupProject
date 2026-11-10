using UnityEngine;

public class PlayerStats : MonoBehaviour, IHasHealth
{
    private float _Hp;
    public float TotalHp = 100;
    private float Hp {
        get { return _Hp; }
        set {
            _Hp = value;
            Gui.UpdateHealth((int)((_Hp / (float)TotalHp) * 100));
        }
    }

    public void Damage(float dmg)
    {
        Hp -= dmg;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hp = TotalHp;
    }

    // Update is called once per frame
    void Update()
    {
        // if Cancel is pressed
        if (Input.GetButtonDown("Cancel"))
        {
            if (GameController.GameState == GameController.State.IN_MENU)
            {
                MenuUI.HideMenu();
            }
            else if (GameController.GameState != GameController.State.IN_MENU)
            {
                MenuUI.CallMenu();
            }
        }
    }
}
