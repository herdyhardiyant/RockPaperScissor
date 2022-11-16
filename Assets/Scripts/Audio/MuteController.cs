using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class MuteController : MonoBehaviour
    {
        // [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Toggle _toggle;

        private void Awake()
        {
            _toggle.onValueChanged.AddListener(SetMute);
            _toggle.isOn = Settings.Settings.GetMute() == 1;
        }
    

        public void SetMute(bool mute)
        {
            AudioListener.pause = mute;
            Settings.Settings.SetMute(mute);
        }
    }
}
