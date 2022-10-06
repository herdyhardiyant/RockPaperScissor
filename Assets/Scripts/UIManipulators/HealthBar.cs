using UnityEngine;
using UnityEngine.UI;

namespace UIManipulators
{
    public class HealthBar : MonoBehaviour
    {
    
        [SerializeField] private Player player;
        [SerializeField] private Image _healthFillImage;

        private void Update()
        {
            _healthFillImage.fillAmount = player.Health / player.MaxHealth;
        }
    
    }
}
