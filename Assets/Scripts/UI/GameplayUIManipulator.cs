using System;
using Gameplay;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameplayUIManipulator : MonoBehaviour
    {
        [SerializeField] private GameObject restartButton;
        [SerializeField] private TMP_Text winnerText;
        [SerializeField] private TMP_Text pingText;
        [SerializeField] private CardGameManager cardGameManager;

        public void SetWinner(string playerName)
        {
            winnerText.text = playerName + " wins!";
            restartButton.SetActive(true);
            winnerText.gameObject.SetActive(true);
        }
        
        private void Awake()
        {
            winnerText.gameObject.SetActive(false);
            restartButton.SetActive(false);
            pingText.gameObject.SetActive(cardGameManager.IsOnline);
        }
        
        
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

     
        void Update()
        {
            if (cardGameManager.IsOnline)
            {
                pingText.text = "Ping: " + PhotonNetwork.GetPing();
            }
        }
    }
}
