using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OnlineConnection
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button leaveRoomButton;
        [SerializeField] private LobbyUI lobbyUI;
        [SerializeField] private RoomUI roomUI;
        
        List<PlayerListItemUI> _playerListInRoom = new List<PlayerListItemUI>();

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
                PlayerListItemUI playerListItemUI = playerObjectUI.GetComponent<PlayerListItemUI>();
                playerListItemUI.Set(player);
                _playerListInRoom.Add(playerListItemUI);

                if (player == PhotonNetwork.LocalPlayer)
                    playerListItemUI.transform.SetAsFirstSibling();
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
            
            // TODO Fix room bug ui
            
            if(PhotonNetwork.CountOfRooms == 0)
                lobbyUI.ClearRoomButtonListUI();
        }
        
        private void StartGame()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.AutomaticallySyncScene = true;
                SceneManager.LoadScene("Gameplay");
                // TODO 26 53:06
            }
        }
        
    }
}
