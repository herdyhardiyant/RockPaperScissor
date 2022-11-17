
using UnityEngine;

namespace Gameplay
{
    public class Bot : MonoBehaviour
    {
        [SerializeField] private CardGameManager cardGameManager;
        private Card []_cards;
        
        
        private void Awake()
        {
            _cards = GetComponentsInChildren<Card>();
        }

        private float _timer = 0;
    
        void Update()
        {
            if (cardGameManager.IsOnline)
            {
                this.enabled = false;
                return;
            }
            
            if (_timer < 0.5)
            {
                _timer += Time.deltaTime;
                return;
            }
        
            var random = Random.Range(0, 3);
            _cards[random].SelectCardSilently();
            _timer = 0;
        }
        
        
        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }
        
        public static Difficulty SelectedDifficulty => _selectedDifficulty;

        private static Difficulty _selectedDifficulty = Difficulty.Easy;

        public static void SetDifficulty(Difficulty difficulty)
        {
            _selectedDifficulty = difficulty;
        }
    }
}
