using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIManipulators
{
    public class MainMenuController : MonoBehaviour
    {

        public void StartGame()
        {
            SceneManager.LoadScene("Gameplay");
        }
        
        public void OpenSettings()
        {
            _OptionsPanel.SetActive(true);
            _MenuButtons.SetActive(false);
        }

        public void CloseSetting()
        {
            _OptionsPanel.SetActive(false);
            _MenuButtons.SetActive(true);
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
        
        [SerializeField]
        private GameObject _OptionsPanel;
        
        [SerializeField]
        private GameObject _MenuButtons;
        
        void Start()
        {
            _OptionsPanel.SetActive(false);
            _MenuButtons.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
