using UnityEngine;

namespace Settings
{
    public class Settings : MonoBehaviour
    {

        private static readonly string _volumeName = "Volume";
        private static readonly string _muteName = "Mute";
    
        public static void SetVolume(float newVolume)
        {
            print("Volume set to " + newVolume);
            PlayerPrefs.SetFloat(_volumeName, newVolume);
        
        }

        public static void SetMute(bool isMute)
        {
            print($"Mute set to {isMute}");
            PlayerPrefs.SetInt(_muteName, isMute ? 1 : 0);
        }

        public static float GetVolume()
        {
            return PlayerPrefs.GetFloat(_volumeName, 1);
        }

        public static int GetMute()
        {
            return PlayerPrefs.GetInt(_muteName, 0);
        }

    }
}
