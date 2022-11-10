using OnlineConnection;
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

        public void Set(LobbyManager manager, string roomName)
        {
            _lobbyManager = manager;
            _roomNameText.text = roomName;
            _roomButton.onClick.AddListener(() => _lobbyManager.JoinRoom(_roomNameText.text));
            gameObject.name = roomName;

        }
    }
}