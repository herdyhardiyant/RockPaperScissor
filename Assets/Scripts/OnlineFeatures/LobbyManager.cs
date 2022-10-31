using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace OnlineFeatures
{
    //TODO Move room script to a different class

    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] TMP_InputField newRoomInputField;
        [SerializeField] TMP_Text feedbackText;
        [SerializeField] private GameObject roomPanel;
        [SerializeField] private TMP_Text roomNameText;
        [SerializeField] private GameObject roomListObject;
        [SerializeField] GameObject roomItemPrefab;
        [SerializeField] PlayerItem playerItemPrefab;
        [SerializeField] GameObject playerListObject;
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button leaveRoomButton;


        List<PlayerItem> _playerItems = new List<PlayerItem>();
        List<RoomItem> _roomButtonList = new List<RoomItem>();


        private void Awake()
        {
            leaveRoomButton.onClick.AddListener(LeaveRoom);
        }

        private void Start()
        {
            PhotonNetwork.JoinLobby();
            feedbackText.text = "";
        }

        public void CreateRoom()
        {
            feedbackText.text = "";

            if (newRoomInputField.text.Length < 1)
            {
                feedbackText.text = "Room name is too short";
                return;
            }

            if (newRoomInputField.text.Length > 20)
            {
                feedbackText.text = "Room name is too long";
                return;
            }

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            PhotonNetwork.CreateRoom(newRoomInputField.text, roomOptions);
            print("Creating room " + newRoomInputField.text);
            BuildRoomButton(newRoomInputField.text);
        }

        public void ClickStartGame(string levelName)
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.LoadLevel(levelName);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            roomPanel.SetActive(false);
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
                BuildRoomButton(room.Name);
            }
        }


        public override void OnCreatedRoom()
        {
            var createdRoomName = PhotonNetwork.CurrentRoom.Name;
            print("Created Room: " + createdRoomName);
            feedbackText.text = "Created Room: " + createdRoomName;
        }

        private void BuildRoomButton(string createdRoomName)
        {
            var newRoomItem = Instantiate(roomItemPrefab, roomListObject.transform, false);
            RoomItem roomItem = newRoomItem.GetComponent<RoomItem>();
            roomItem.Set(this, createdRoomName);
            _roomButtonList.Add(roomItem);
            
        }

        public override void OnJoinedRoom()
        {
            print("Joined Room " + PhotonNetwork.CurrentRoom.Name);
            feedbackText.text = "Joined Room: " + PhotonNetwork.CurrentRoom.Name;
            roomNameText.text = PhotonNetwork.CurrentRoom.Name;
            roomPanel.SetActive(true);

            UpdatePlayerList();

            // start game button
            // when player count is 2
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
            // start game button
            // when player count is 2
            SetupStartButton();
        }

        private void SetupStartButton()
        {
            startGameButton.GameObject().SetActive(PhotonNetwork.IsMasterClient);
            startGameButton.interactable = PhotonNetwork.CurrentRoom.PlayerCount > 1;
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
                PlayerItem playerItem = Instantiate(playerItemPrefab, playerListObject.transform, false);
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