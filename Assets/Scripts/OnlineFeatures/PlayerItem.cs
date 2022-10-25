using Photon.Pun;
using TMPro;
using UnityEngine;

namespace OnlineFeatures
{
    public class PlayerItem : MonoBehaviour
    {
        
        [SerializeField] TMP_Text playerName;

        public void Set(Photon.Realtime.Player player)
        {
            playerName.text = player.NickName;
            
            if (Equals(player, PhotonNetwork.MasterClient))
            {
                playerName.text += " (Master)";
            }
        }
        
    }
}
