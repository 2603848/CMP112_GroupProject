using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenModeDropdown : MonoBehaviour
{
    static int NumModes = 4;
    TMP_Dropdown Dropdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Dropdown = GetComponent<TMP_Dropdown>();
        Dropdown.ClearOptions();

        List<string> options = new List<string>();
        int selectedIndex = 0;
        FullScreenMode currentMode = PlayerPrefs.HasKey("fullscreen_mode") ? (FullScreenMode)PlayerPrefs.GetInt("fullscreen_mode") : Screen.fullScreenMode;
        for (int i = 0; i < NumModes; i++)
        {
            FullScreenMode mode = (FullScreenMode)i;
            options.Add(mode.ToString());

            // If we found the resolution we're looking for in the dropdown list
            if (mode == currentMode)
            {
                selectedIndex = i;
            }
        }
        Dropdown.AddOptions(options);
        Dropdown.value = selectedIndex;
        // Save the currently set resolution
        PlayerPrefs.SetInt("fullscreen_mode", (int)currentMode);
        Screen.fullScreenMode = currentMode;

        if (currentMode == FullScreenMode.ExclusiveFullScreen || currentMode == FullScreenMode.FullScreenWindow)
            Screen.fullScreen = true;
        else
            Screen.fullScreen = false;
    }
    public void DropdownValueChanged(Int32 id)
    {
        FullScreenMode mode = (FullScreenMode)Dropdown.value;
        Screen.fullScreenMode = mode;
        PlayerPrefs.SetInt("fullscreen_mode", (int)mode);
    }
}
