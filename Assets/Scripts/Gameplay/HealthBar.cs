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
            _healthFillImage.fillAmount = cardPlayer.Health / cardPlayer.MaxHealth;
        }
    
    }
}