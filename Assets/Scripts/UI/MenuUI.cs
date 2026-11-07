using UnityEditor;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject OptionsPanel;
    public GameObject[] SubPanels;

    static MenuUI SingletonInstance;

    public Canvas canvas;

    // Show the entire UI parent
    public static void CallMenu()
    {
        GameController.GameState = GameController.State.IN_MENU;
        SingletonInstance.canvas.enabled = true;
        SingletonInstance.gameObject.SetActive(true);

        foreach (GameObject g in SingletonInstance.SubPanels)
        {
            g.SetActive(false);
        }
    }
    // Hide the entire UI parent
    public static void HideMenu()
    {
        GameController.GameState = GameController.State.IN_GAME;
        SingletonInstance.canvas.enabled = false;
        SingletonInstance.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }
    // Show options panel
    public void CallOptions()
    {
        foreach (GameObject g in SubPanels)
            g.SetActive(false);
        OptionsPanel.SetActive(true);

        for (int i = 0; i < OptionsPanel.transform.childCount; i++)
        {
            GameObject g = OptionsPanel.transform.GetChild(i).gameObject;
            g.SetActive(true);
        }
    }
    // Hide options panel
    public void HideOptions()
    {
        OptionsPanel.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SingletonInstance = this;
        HideMenu();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
