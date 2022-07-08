using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    public Button multiplayerButton;

    bool isConnected = false;
    
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.NickName = "Player Name";
        PhotonNetwork.GameVersion = Application.version.ToString();

        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        multiplayerButton.interactable = isConnected;
    }

    public override void OnConnected()
    {
        base.OnConnected();
        Debug.Log("Is Connected");
        isConnected = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("Is Disconnected, " + cause);
        isConnected = false;
    }
}
