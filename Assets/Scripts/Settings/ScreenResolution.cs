using TMPro;
using UnityEngine;

namespace Settings
{
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
}
