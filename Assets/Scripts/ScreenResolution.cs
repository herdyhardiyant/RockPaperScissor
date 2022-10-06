using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolution : MonoBehaviour
{
    
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;

    void Start()
    {
        resolutionDropdown.onValueChanged.AddListener(ValueChangeHandler);
    }

    private void ValueChangeHandler(int selectedMenu)
    {
      var selectedMenuText = resolutionDropdown.options[selectedMenu].text;
      print("Screen resolution is changed to: " + selectedMenuText);
    }

}
