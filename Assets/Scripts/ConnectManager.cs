using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text usernameInput;
    [SerializeField] private TMP_Text feedbackText;
    
    //TODO Click Connect
    
    public void Connect()
    {
        //TODO Check player username
        
        //Check if username is empty
        if (string.IsNullOrEmpty(usernameInput.text))
        {
            feedbackText.text = "Please enter a username";
            return;
        }
        
        //Check if username is too long
        if (usernameInput.text.Length > 10)
        {
            feedbackText.text = "Username is too long";
            return;
        }
        
        //Check if username is too short
        if (usernameInput.text.Length < 3)
        {
            feedbackText.text = "Username is too short";
            return;
        }
        
        PhotonNetwork.NickName = usernameInput.text;

        PhotonNetwork.ConnectUsingSettings();
        feedbackText.text = "Connecting to server...";
    }

    public override void OnConnectedToMaster()
    {
        feedbackText.text = "Connected to server";
        SceneManager.LoadScene("Lobby");
    }

    //TODO feedback connection status on connected to master
    
    
}
