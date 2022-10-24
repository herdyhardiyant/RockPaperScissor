using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    [SerializeField] TMP_Text roomNameText;
    LobbyManager _lobbyManager;

    public void Set(LobbyManager manager, string roomName)
    {
        _lobbyManager = manager;
        roomNameText.text = roomName;
        
    }
    
    public void OnClick()
    {
        _lobbyManager.JoinRoom(roomNameText.text);
        
    }
    
}
