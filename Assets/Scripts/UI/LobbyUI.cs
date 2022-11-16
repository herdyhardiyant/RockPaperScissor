using System;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LobbyUI : MonoBehaviour
    {
        [SerializeField] TMP_Text feedbackText;
        [SerializeField] private GameObject roomPanel;
        [SerializeField] private TMP_Text roomNameText;
        [SerializeField] private GameObject roomListUI;
        [SerializeField] GameObject roomPrefab;
        List<GameObject> _roomButtonList = new List<GameObject>();
        [SerializeField] private TMP_InputField roomNameInput;
        
        
        

        public GameObject AddRoomOnRoomListUI()
        {
            var newRoomItem = Instantiate(roomPrefab, roomListUI.transform, false);
            _roomButtonList.Add(newRoomItem);
            return newRoomItem;
        }

        public void ClearRoomButtonListUI()
        {
            print("Clearing room list");
            foreach (var item in _roomButtonList)
            {
                Destroy(item.gameObject);
            }

            _roomButtonList.Clear();
        }

        public void ResetCreateRoomUI()
        {
            feedbackText.text = "";
            roomNameText.text = "";
            roomNameInput.text = "";
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