using System;
using Photon.Pun;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace OnlineFeatures
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button leaveRoomButton;
        [SerializeField] private LobbyUI lobbyUI;

        private void Awake()
        {
            leaveRoomButton.onClick.AddListener(LeaveRoom);
            startGameButton.onClick.AddListener(StartGame);
        }

        private void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            lobbyUI.HideRoomPanel();
        }
        
        private void StartGame()
        {
            if (PhotonNetwork.IsMasterClient)
                print("Starting game");
                // PhotonNetwork.LoadLevel();
        }
        
    }
}
