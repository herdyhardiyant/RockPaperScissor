using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UI;
using UnityEngine;

namespace OnlineConnection
{

    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] TMP_InputField newRoomInputField;
        [SerializeField] private LobbyUI lobbyUI;

        List<RoomButton> _roomButtonList = new List<RoomButton>();
        
        private void Awake()
        {
           var isJoined = PhotonNetwork.JoinLobby();
           print("isJoined: " + isJoined);
        }

        public override void OnConnectedToMaster()
        {
            lobbyUI.SetFeedbackText("Connected to master server.");

          var isJoined =   PhotonNetwork.JoinLobby();
          print("isJoined: " + isJoined);

          if (isJoined)
          {
              lobbyUI.SetFeedbackText("Joined lobby.");
          }
          else
          {
             lobbyUI.SetFeedbackText("Failed to join lobby.");
          }

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
        
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            // print("Room list updated");
            
            lobbyUI.ClearRoomButtonListUI();
            
            foreach (RoomInfo room in roomList)
            {
                if (room.PlayerCount == 0)
                {
                    room.RemovedFromList = true;
                    return;
                }
                CreateRoom(room.Name);
            }
          
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            var createdRoomName = PhotonNetwork.CurrentRoom.Name;
            // print("Created Room: " + createdRoomName);
            lobbyUI.SetFeedbackText("Created Room: " + createdRoomName);
        }

        private void CreateRoom(string createdRoomName)
        {
            var newRoomItemObject = lobbyUI.AddRoomOnRoomListUI();
            RoomButton roomButton = newRoomItemObject.GetComponent<RoomButton>();
            roomButton.Set(this, createdRoomName);
        }

        public void JoinRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }
}