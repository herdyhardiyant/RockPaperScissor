
using UnityEngine;

namespace Gameplay
{
    public class Bot : MonoBehaviour
    {
    
        private Card []_cards;
    
        private void Awake()
        {
            _cards = GetComponentsInChildren<Card>();
        }

        private float _timer = 0;
    
        void Update()
        {
            if (_timer < 0.5)
            {
                _timer += Time.deltaTime;
                return;
            }
        
            var random = Random.Range(0, 3);
            _cards[random].SelectCardSilently();
            _timer = 0;
        }
    }
}
