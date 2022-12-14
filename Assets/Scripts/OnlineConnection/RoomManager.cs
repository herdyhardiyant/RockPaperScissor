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
            base.OnJoinedRoom();
            lobbyUI.SetFeedbackText("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
            lobbyUI.ShowJoinedRoomPanel(PhotonNetwork.CurrentRoom.Name);

            UpdatePlayerList();
            UpdateStartButton();

        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            UpdatePlayerList();
            UpdateStartButton();

        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            UpdatePlayerList();
            UpdateStartButton();
            
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
            base.OnMasterClientSwitched(newMasterClient);
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
            lobbyUI.SetFeedbackText("Leave room and returning to master server...");
        }
        
        private void StartGame()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.AutomaticallySyncScene = true;
                SceneManager.LoadScene("Gameplay");
            }
        }
        
    }
}
