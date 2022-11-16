using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
 
        [SerializeField]
        private Button resumeButton;

        [SerializeField]
        private Button quitButton;
    
    
        public void CloseMenu()
        {
            gameObject.SetActive(false);
        }
    
        private void Start()
        {
            gameObject.SetActive(false);
            resumeButton.onClick.AddListener(CloseMenu);
            quitButton.onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
            SceneManager.LoadScene("StartMenu");
        }
    
    
    }
}