using System;
using OnlineFeatures;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LobbyUI : MonoBehaviour
    {
        
        [SerializeField] TMP_Text feedbackText;
        [SerializeField] private GameObject roomPanel;
        [SerializeField] private TMP_Text roomNameText;
        [SerializeField] private GameObject roomListUI;
        [SerializeField] GameObject roomPrefab;

        
        public GameObject AddRoomOnRoomListUI(LobbyManager lobbyManager)
        {
            var newRoomItem = Instantiate(roomPrefab, roomListUI.transform, false);
            return newRoomItem;
        }
        
        public void SetFeedbackText(string text)
        {
            feedbackText.text = text;
        }

        public void HideRoomPanel()
        {
            roomPanel.SetActive(false);
        }
        
        public void ShowJoinedRoomPanel(string roomName)
        {
            roomNameText.text = roomName;
            roomPanel.SetActive(true);
        }

        private void Awake()
        {
            roomPanel.SetActive(false);
            feedbackText.text = "";
        }
        
        
    }
}
