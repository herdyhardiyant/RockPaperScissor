using MultiplayerFeatures;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerListItemUI : MonoBehaviour
    {
        
        [SerializeField] TMP_Text playerName;
        [SerializeField] Image playerAvatarImage;
        
        public void Set(Photon.Realtime.Player player)
        {
            playerName.text = player.NickName;
            
            if (Equals(player, PhotonNetwork.MasterClient))
            {
                playerName.text += " (Master)";
            }
            
            var avatar = (int) player.CustomProperties[AvatarSelection.SelectedAvatarIndexPropertyName];
            playerAvatarImage.sprite = AvatarSpritesCollection.GetAvatar(avatar);
            playerAvatarImage.SetNativeSize();
            playerAvatarImage.transform.localScale = new Vector3(3,3,1);
        }
        
    }
}
