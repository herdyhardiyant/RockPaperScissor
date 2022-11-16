using OnlineConnection;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RoomButton : MonoBehaviour
    {
       
        private TMP_Text _roomNameText;
        private Button _roomButton;
        private LobbyManager _lobbyManager;

        private void Awake()
        {
             _roomButton = GetComponent<Button>();
             _roomNameText = GetComponentInChildren<TMP_Text>();
        }

        public void Set(LobbyManager manager, RoomInfo createdRoomInfo)
        {
            var isRoomFull = createdRoomInfo.PlayerCount == createdRoomInfo.MaxPlayers;
            _lobbyManager = manager;
            _roomNameText.text = createdRoomInfo.Name + " (" + createdRoomInfo.PlayerCount + "/" + createdRoomInfo.MaxPlayers + ")";
            _roomButton.onClick.AddListener(() => _lobbyManager.JoinRoom(createdRoomInfo.Name));
            gameObject.name = createdRoomInfo.Name;
            _roomButton.interactable = !isRoomFull;
        }
        
        
        
        
        
    }
}