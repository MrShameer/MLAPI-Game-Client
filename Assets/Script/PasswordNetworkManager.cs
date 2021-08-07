using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using System.Text;
using UnityEngine.UI;
using MLAPI.SceneManagement;
using System;

public class PasswordNetworkManager : MonoBehaviour
{
    [SerializeField] private InputField passwordField;
    [SerializeField] private GameObject passwordEntry;
    [SerializeField] private GameObject leaveButton;
    
    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += HandleServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnect;
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton == null) { return; }
        
        NetworkManager.Singleton.OnServerStarted -= HandleServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;
    }

    public void Host()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost(new Vector3(-2f, 2f, 0f), Quaternion.Euler(0f, 0f, 0f));
        NetworkSceneManager.SwitchScene("Lobby"); //aku tambah
    }

    public void Client()
    {
        NetworkManager.Singleton.NetworkConfig.ConnectionData = Encoding.ASCII.GetBytes(passwordField.text);
        NetworkManager.Singleton.StartClient();
    }

    public void Leave()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.StopHost();
            NetworkManager.Singleton.ConnectionApprovalCallback -= ApprovalCheck;
        }
        else if (NetworkManager.Singleton.IsClient)
        {
            NetworkManager.Singleton.StopClient();
        }

        passwordEntry.SetActive(true);
        leaveButton.SetActive(false);
    }

    private void HandleServerStarted()
    {
        // Temporary workaround to treat host as client
        if (NetworkManager.Singleton.IsHost)
        {
            HandleClientConnected(NetworkManager.Singleton.LocalClientId);
        }
    }

    private void HandleClientConnected(ulong clientId)
    {
        // Are we the client that is connecting?
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            passwordEntry.SetActive(false);
            leaveButton.SetActive(true);
            //teamPickerUI.SetActive(true);
        }
    }

    private void HandleClientDisconnect(ulong clientId)
    {

        // Are we the client that is disconnecting?
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            passwordEntry.SetActive(true);
            leaveButton.SetActive(false);
            //teamPickerUI.SetActive(false);
        }
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientId, NetworkManager.ConnectionApprovedDelegate callback)
    {
        string password = Encoding.ASCII.GetString(connectionData);

        bool approveConnection = password == passwordField.text;

        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;

        switch (NetworkManager.Singleton.ConnectedClients.Count)
        {
            case 1:
                spawnPos = new Vector3(0f, 0f, 0f);
                spawnRot = Quaternion.Euler(0f, 180f, 0f);
                break;
            case 2:
                spawnPos = new Vector3(2f, 0f, 0f);
                spawnRot = Quaternion.Euler(0f, 225, 0f);
                break;
        }

        callback(true, null, approveConnection, spawnPos, spawnRot);
    }
}
