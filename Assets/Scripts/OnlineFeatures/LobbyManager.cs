using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace OnlineFeatures
{
    //TODO Move room script to a different class

    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] TMP_InputField newRoomInputField;
        [SerializeField] private LobbyUI lobbyUI;
        [SerializeField] private RoomUI roomUI;



        List<PlayerItem> _playerItems = new List<PlayerItem>();
        List<RoomItem> _roomButtonList = new List<RoomItem>();
        

        private void Start()
        {
            PhotonNetwork.JoinLobby();
        }

        public void CreateRoom()
        {
            if (newRoomInputField.text.Length < 1)
            {
                lobbyUI.SetFeedbackText("Room name is too short");
                return;
            }

            if (newRoomInputField.text.Length > 20)
            {
                lobbyUI.SetFeedbackText("Room name is too long");
                return;
            }

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            PhotonNetwork.CreateRoom(newRoomInputField.text, roomOptions);
            lobbyUI.SetFeedbackText("Creating room" + newRoomInputField.text);
            CreateRoom(newRoomInputField.text);
        }

        public void ClickStartGame(string levelName)
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.LoadLevel(levelName);
        }
        
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (var item in _roomButtonList)
            {
                Destroy(item.gameObject);
            }

            _roomButtonList.Clear();

            foreach (RoomInfo room in roomList)
            {
                CreateRoom(room.Name);
            }
        }


        public override void OnCreatedRoom()
        {
            var createdRoomName = PhotonNetwork.CurrentRoom.Name;
            print("Created Room: " + createdRoomName);
            lobbyUI.SetFeedbackText("Created Room: " + createdRoomName);
        }

        private void CreateRoom(string createdRoomName)
        {
            var newRoomItemObject = lobbyUI.AddRoomOnRoomListUI(this);
            RoomItem roomItem = newRoomItemObject.GetComponent<RoomItem>();
            roomItem.Set(this, createdRoomName);
            _roomButtonList.Add(roomItem);
        }

        public override void OnJoinedRoom()
        {
            lobbyUI.SetFeedbackText("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
            lobbyUI.ShowJoinedRoomPanel(PhotonNetwork.CurrentRoom.Name);

            UpdatePlayerList();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            UpdatePlayerList();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            UpdatePlayerList();
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            SetupStartButton();
        }

        private void SetupStartButton()
        {
            var isButtonActive = PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1;
            roomUI.SetStartGameButtonActive(isButtonActive);
        }

        private void UpdatePlayerList()
        {
            foreach (var item in _playerItems)
            {
                Destroy(item.gameObject);
            }

            _playerItems.Clear();

            foreach (Player player in PhotonNetwork.PlayerList)
            {
                var playerObjectUI = roomUI.AddPlayerItemOnPlayerListInRoomUI();
                PlayerItem playerItem = playerObjectUI.GetComponent<PlayerItem>();
                playerItem.Set(player);
                _playerItems.Add(playerItem);

                if (player == PhotonNetwork.LocalPlayer)
                    playerItem.transform.SetAsFirstSibling();
            }
        }

        public void JoinRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }
}