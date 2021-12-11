using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class JoinGame : MonoBehaviour
{
    [Header("Pre Join")]
    public GameObject lobbyUI;
    public TMP_InputField clientIp;
    //UNetTransport transport;
    [SerializeField] private string ip = "localhost";

    [Header("Post Join")]
    public GameObject readyPanel;
    public GameObject leaveButton;
    public GameObject startButton;

    private void Start()
    {
        lobbyUI.SetActive(true);
    }

    public void Host()
    {
        // TO DO 

        //transport = NetworkManager.singleton.GetComponent<kcp2k.KcpTransport>();

        //transport.ConnectAddress = ip;
        NetworkManager.singleton.networkAddress = ip;
        Debug.Log(NetworkManager.singleton.networkAddress);
        lobbyUI.SetActive(false);
        leaveButton.SetActive(true);

        startButton.SetActive(true);

        NetworkManager.singleton.StartHost();
        Debug.Log(NetworkManager.singleton.networkAddress);

    }

    public void Client()
    {
        // TO DO 
        //transport = NetworkManager.Singleton.GetComponent<UNetTransport>();

        //transport.ConnectAddress = ip;
        NetworkManager.singleton.networkAddress = ip;
        lobbyUI.SetActive(false);
        leaveButton.SetActive(true);

        NetworkManager.singleton.StartClient();
    }

    public void Leave()
    {
        // TO DO 
        //if (NetworkManager.singleton.isHost)
        //{
        //    NetworkManager.singleton.StopHost();
        //}
        //else if (NetworkManager.singleton.IsClient)
        //{
        //    NetworkManager.singleton.StopClient();
        //}

        leaveButton.SetActive(false);

        Application.Quit();
        return;
    }

    public void ChangeIP(string i)
    {
        ip = i;
    }
}

