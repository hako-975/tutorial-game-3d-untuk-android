using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI namaRoom; 
    public TextMeshProUGUI pemainRoom;
    public TextMeshProUGUI pingText;

    public PlayerControllerNetwork playerNetworkPrefab;

    [HideInInspector]
    public PlayerControllerNetwork localPlayerNetwork;

    void Start()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            SceneManager.LoadScene("Main Menu");
            return;
        }

        PlayerControllerNetwork.RefreshInstance(ref localPlayerNetwork, playerNetworkPrefab);

        namaRoom.text = "Nama Room: " + PhotonNetwork.CurrentRoom.Name;
        pemainRoom.text = "Pemain: " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    private void Update()
    {
        pingText.text = PhotonNetwork.GetPing() + " ms";
    }

    public void OnClickLeaveRoom()
    {
        Time.timeScale = 1f;
        StartCoroutine(WaitLeaveRoom());
    }

    IEnumerator WaitLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

        while (PhotonNetwork.InRoom)
        {
            yield return null;
        }

        PhotonNetwork.JoinLobby();
        SceneManager.LoadScene("Lobby");
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        SceneManager.LoadScene("Lobby");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        pemainRoom.text = "Pemain: " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        pemainRoom.text = "Pemain: " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }
}
