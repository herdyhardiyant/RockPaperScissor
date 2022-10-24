using TMPro;
using UnityEngine;

namespace OnlineFeatures
{
    public class RoomItem : MonoBehaviour
    {
        [SerializeField] TMP_Text roomNameText;
        LobbyManager _lobbyManager;

        public void Set(LobbyManager manager, string roomName)
        {
            _lobbyManager = manager;
            roomNameText.text = roomName;
        
        }
    
        public void OnClick()
        {
            _lobbyManager.JoinRoom(roomNameText.text);
            
        }
    
    }
}
