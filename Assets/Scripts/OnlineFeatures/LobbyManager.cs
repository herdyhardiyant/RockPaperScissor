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

        List<RoomButtonManipulator> _roomButtonList = new List<RoomButtonManipulator>();
        

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

        public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
            print("Lobby stats updated");
        }
        
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            print("Room list updated");
            lobbyUI.ClearRoomButtonListUI();

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
            var newRoomItemObject = lobbyUI.AddRoomOnRoomListUI();
            RoomButtonManipulator roomButtonManipulator = newRoomItemObject.GetComponent<RoomButtonManipulator>();
            roomButtonManipulator.Set(this, createdRoomName);
        }

        public void JoinRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }
}