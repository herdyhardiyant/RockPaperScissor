using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OnlineFeatures
{
   public class LobbyManager : MonoBehaviourPunCallbacks
   {
      [SerializeField] TMP_InputField newRoomInputField;
      [SerializeField] TMP_Text feedbackText;
      [SerializeField] private GameObject roomPanel;
      [SerializeField] private TMP_Text roomNameText;
      [SerializeField] private GameObject roomListObject;
      [SerializeField] GameObject roomItemPrefab;
   
      List<RoomItem> _roomList = new List<RoomItem>();


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
      
      }

      public void ExitRoom()
      {
         PhotonNetwork.LeaveRoom();
      }

      public override void OnRoomListUpdate(List<RoomInfo> roomList)
      {
         foreach (var item in _roomList)
         {
            Destroy(item.gameObject);
         }
      
         _roomList.Clear();
      
         foreach (RoomInfo room in roomList)
         {
            CreateRoomItem(room.Name);
         }
      }
      
      

      public override void OnCreatedRoom()
      {
         var createdRoomName = PhotonNetwork.CurrentRoom.Name;
         
         CreateRoomItem(createdRoomName);

         print("Created Room: " + createdRoomName);
         feedbackText.text = "Created Room: " + createdRoomName;
      }

      private void CreateRoomItem(string createdRoomName)
      {
         var newRoomItem = Instantiate(roomItemPrefab, roomListObject.transform, false);
         newRoomItem.GetComponentInChildren<Button>().onClick.AddListener(() => { JoinRoom(createdRoomName); });
         RoomItem roomItem = newRoomItem.GetComponent<RoomItem>();
         roomItem.Set(this, createdRoomName);
         _roomList.Add(roomItem);
      }

      public override void OnJoinedRoom()
      {
         print("Joined Room " + PhotonNetwork.CurrentRoom.Name);
         feedbackText.text = "Joined Room: " + PhotonNetwork.CurrentRoom.Name;
         roomNameText.text = PhotonNetwork.CurrentRoom.Name;
         roomPanel.SetActive(true);
      
      }


      public void JoinRoom(string roomName)
      {
         PhotonNetwork.JoinRoom(roomName);
      }
  
   }
}
