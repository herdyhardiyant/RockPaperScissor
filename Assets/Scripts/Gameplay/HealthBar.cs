using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class HealthBar : MonoBehaviour
    {
    
        [SerializeField] private CardPlayer cardPlayer;
        [SerializeField] private Image _healthFillImage;

        private void Update()
        {
            print(cardPlayer.name);
            print(cardPlayer.Health);
            print("Max Health: " + cardPlayer.MaxHealth);
            _healthFillImage.fillAmount = cardPlayer.Health / cardPlayer.MaxHealth;
        }
    
    }
}