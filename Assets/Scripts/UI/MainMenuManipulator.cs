using Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuManipulator : MonoBehaviour
    {
        [SerializeField] private Button easyButton;
        [SerializeField] private Button mediumButton;
        [SerializeField] private Button hardButton;
        [SerializeField]
        private GameObject _OptionsPanel;
        
        [SerializeField]
        private GameObject _MenuButtons;
        
        public void StartGameWithDifficultySetting(Bot.Difficulty difficulty)
        {
            Bot.SetDifficulty(difficulty);
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
       
        
        void Awake()
        {
            _OptionsPanel.SetActive(false);
            _MenuButtons.SetActive(true);
            
            easyButton.onClick.AddListener(() => StartGameWithDifficultySetting(Bot.Difficulty.Easy));
            mediumButton.onClick.AddListener(() => StartGameWithDifficultySetting(Bot.Difficulty.Medium));
            hardButton.onClick.AddListener(() => StartGameWithDifficultySetting(Bot.Difficulty.Hard));
            
        }
    }
}