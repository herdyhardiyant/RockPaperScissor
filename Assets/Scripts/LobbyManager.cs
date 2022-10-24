using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
   [SerializeField] TMP_InputField newRoomInputField;
   [SerializeField] TMP_Text feedbackText;
   [SerializeField] private GameObject roomPanel;
   [SerializeField] private TMP_Text roomNameText;
   [SerializeField] private GameObject roomListObject;
   [SerializeField] RoomItem roomItemPrefab;
   
   List<RoomItem> _roomList = new List<RoomItem>();


   private void Start()
   {
      PhotonNetwork.JoinLobby();
      
   }

   public void CreateRoom()
   {
      feedbackText.text = "";
      //Check if the room name is valid
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

   public override void OnRoomListUpdate(List<RoomInfo> roomList)
   {
      foreach (var item in _roomList)
      {
         Destroy(item.gameObject);
      }
      
      _roomList.Clear();
      
      foreach (RoomInfo room in roomList)
      {
         RoomItem newRoomItem = Instantiate(roomItemPrefab);
         newRoomItem.Set(this, room.Name);
         _roomList.Add(newRoomItem);
      }
   }

   public override void OnCreatedRoom()
   {
      print("Created Room: " + PhotonNetwork.CurrentRoom.Name);
      feedbackText.text = "Created Room: " + PhotonNetwork.CurrentRoom.Name;
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
