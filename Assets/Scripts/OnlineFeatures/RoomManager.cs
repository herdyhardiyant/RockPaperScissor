using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace OnlineFeatures
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button leaveRoomButton;
        [SerializeField] private LobbyUI lobbyUI;
        [SerializeField] private RoomUI roomUI;
        
        List<PlayerItem> _playerListInRoom = new List<PlayerItem>();

        private void Awake()
        {
            leaveRoomButton.onClick.AddListener(LeaveRoom);
            startGameButton.onClick.AddListener(StartGame);
        }
        
        
        
        public override void OnJoinedRoom()
        {
            lobbyUI.SetFeedbackText("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
            lobbyUI.ShowJoinedRoomPanel(PhotonNetwork.CurrentRoom.Name);

            UpdatePlayerList();
            UpdateStartButton();

        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            UpdatePlayerList();
            UpdateStartButton();

        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            UpdatePlayerList();
            UpdateStartButton();
           
            // Get current room
            Room currentRoom = PhotonNetwork.CurrentRoom;
            
            print(currentRoom.Name);

        }
        
        private void UpdatePlayerList()
        {

            foreach (var item in _playerListInRoom)
            {
                Destroy(item.gameObject);
            }

            _playerListInRoom.Clear();

            foreach (Player player in PhotonNetwork.PlayerList)
            {
                var playerObjectUI = roomUI.AddPlayerItemOnPlayerListInRoomUI();
                PlayerItem playerItem = playerObjectUI.GetComponent<PlayerItem>();
                playerItem.Set(player);
                _playerListInRoom.Add(playerItem);

                if (player == PhotonNetwork.LocalPlayer)
                    playerItem.transform.SetAsFirstSibling();
            }
        }
        
        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            UpdateStartButton();
        }

        private void UpdateStartButton()
        {
            var isButtonActive = PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1;
            roomUI.SetStartGameButtonActive(isButtonActive);
        }
        
        private void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            lobbyUI.HideRoomPanel();
            lobbyUI.ResetCreateRoomUI();
            PhotonNetwork.JoinLobby();
            
            if(PhotonNetwork.CountOfRooms == 0)
                lobbyUI.ClearRoomButtonListUI();
        }
        
        private void StartGame()
        {
            if (PhotonNetwork.IsMasterClient)
                print("Starting game");
                // PhotonNetwork.LoadLevel();
        }
        
    }
}
