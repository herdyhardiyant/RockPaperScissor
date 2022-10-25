using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OnlineFeatures
{
    public class RoomItem : MonoBehaviour
    {
        [SerializeField] TMP_Text roomNameText;
        [SerializeField] private Button roomButton;
        LobbyManager _lobbyManager;

        public void Set(LobbyManager manager, string roomName)
        {
            _lobbyManager = manager;
            roomNameText.text = roomName;
            
            roomButton.onClick.AddListener( () => _lobbyManager.JoinRoom(roomName));
        }
    
        public void OnClick()
        {
            _lobbyManager.JoinRoom(roomNameText.text);
            
        }
    
    }
}
