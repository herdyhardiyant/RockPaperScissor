using ExitGames.Client.Photon;
using MultiplayerFeatures;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OnlineConnection
{
    public class ConnectManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text usernameInput;
        [SerializeField] private TMP_Text feedbackText;
        [SerializeField] private AvatarSelection avatarSelection;
        
        public void Connect()
        {
            //Check if username is empty
            if (string.IsNullOrEmpty(usernameInput.text))
            {
                feedbackText.text = "Please enter a username";
                return;
            }
        
            //Check if username is too long
            if (usernameInput.text.Length > 10)
            {
                feedbackText.text = "Username is too long";
                return;
            }
        
            //Check if username is too short
            if (usernameInput.text.Length < 3)
            {
                feedbackText.text = "Username is too short";
                return;
            }
        
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.NickName = usernameInput.text;
            PhotonNetwork.ConnectUsingSettings();

            var playerProperties = new Hashtable();
            
            playerProperties.Add(AvatarSelection.SelectedAvatarIndexPropertyName, avatarSelection.SelectedAvatarIndex);
            
           var isPropertySet =  PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);

           print("Is property set: " + isPropertySet);
           
            feedbackText.text = "Connecting to server...";
        }
    
        
        
         public override void OnConnectedToMaster()
        {
            feedbackText.text = "Connected to server";
            SceneManager.LoadScene("Lobby");
            
        }

    }
}
