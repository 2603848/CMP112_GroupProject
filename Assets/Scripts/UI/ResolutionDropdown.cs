using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ResolutionDropdown : MonoBehaviour
{
    TMP_Dropdown Dropdown;
    // The default behaviour of Resolution.ToString() returns a very long float for the refresh rate
    // e.g. 1920 x 1200 @ 60.0004006410256 Hz
    // This function is here to simplify the display to an int
    static string GetResolutionString(Resolution r)
    {
        string str = r.width + "x" + r.height + " @ " + (int)r.refreshRateRatio.value;
        return str;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Dropdown = GetComponent<TMP_Dropdown>();
        Dropdown.ClearOptions();

        List<string> options = new List<string>();
        int selectedIndex = 0;
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            Resolution res = Screen.resolutions[i];

            options.Add(GetResolutionString(res));
            if (res.ToString() == GetResolutionString(Screen.currentResolution))
            {
                selectedIndex = i;
            }
        }
        Dropdown.AddOptions(options);
        Dropdown.value = selectedIndex;
    }
    public void DropdownValueChanged(Int32 id)
    {
        Resolution res = Screen.resolutions[Dropdown.value];
        Screen.SetResolution(res.width, res.height, FullScreenMode.MaximizedWindow, res.refreshRateRatio);
    }

}
