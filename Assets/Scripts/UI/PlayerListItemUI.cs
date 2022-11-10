using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerListItemUI : MonoBehaviour
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
