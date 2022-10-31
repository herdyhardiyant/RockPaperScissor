using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OnlineFeatures
{
    public class RoomItem : MonoBehaviour
    {
        private TMP_Text _roomNameText;
        private Button _roomButton;
        LobbyManager _lobbyManager;


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
        }

        public void OnClick()
        {
            _lobbyManager.JoinRoom(_roomNameText.text);
        }
    }
}