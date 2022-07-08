using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public GameObject createRoomPanel;
    public GameObject joinRoomPanel;

    public TMP_InputField roomName;
    public TMP_InputField maxPlayer;

    public TMP_InputField roomNameJoin;

    public TMP_InputField nickName;

    public GameObject messagePanel;
    public TextMeshProUGUI messageText;

    // Start is called before the first frame update
    void Start()
    {
        // set panel awal
        createRoomPanel.SetActive(true);
        joinRoomPanel.SetActive(false);

        nickName.text = PlayerPrefsManager.instance.GetNickName();
    }

    public void ChangePanelToCreateRoom()
    {
        createRoomPanel.SetActive(true);
        joinRoomPanel.SetActive(false);
    }

    public void ChangePanelToJoinRoom()
    {
        joinRoomPanel.SetActive(true);
        createRoomPanel.SetActive(false);
    }

    public void OnClickCreateRoom()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            return;
        }

        byte.TryParse(maxPlayer.text.ToString(), out byte max);
        
        byte minNumberPlayer = 2;
        byte maxNumberPlayer = 20;


        if (max < minNumberPlayer)
        {
            messagePanel.SetActive(true);
            messageText.text = "Maksimal Pemain tidak boleh kurang dari 2!";
            return;
        }

        if (max > maxNumberPlayer)
        {
            messagePanel.SetActive(true);
            messageText.text = "Maksimal Pemain tidak boleh lebih dari 20!";
            return;
        }

        if (string.IsNullOrEmpty(roomName.text.ToString()))
        {
            messagePanel.SetActive(true);
            messageText.text = "Nama Room tidak boleh kosong!";
            return;
        }
        else
        {
            PhotonNetwork.CreateRoom(roomName.text.ToString(), new Photon.Realtime.RoomOptions { MaxPlayers = max });
        }
    }

    public void OnClickJoinRoom()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            return;
        }

        if (string.IsNullOrEmpty(roomNameJoin.text.ToString()))
        {
            messagePanel.SetActive(true);
            messageText.text = "Nama Room Join tidak boleh kosong!";
            return;
        }
        else
        {
            PhotonNetwork.JoinRoom(roomNameJoin.text.ToString());
        }
    }

    public void OnClickQuickJoinRoom()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            return;
        }

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        SceneManager.LoadScene("Multiplayer");
    }

    public void OnClickMainMenuButtonToDisconnect()
    {
        StartCoroutine(MainMenuToDisconnect());
    }

    IEnumerator MainMenuToDisconnect()
    {
        PhotonNetwork.Disconnect();

        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }

        SceneManager.LoadScene("Main Menu");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        messagePanel.SetActive(true);
        messageText.text = "Is Disconnected, " + cause;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        messagePanel.SetActive(true);
        messageText.text = message.ToString();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        messagePanel.SetActive(true);
        messageText.text = message.ToString();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        messagePanel.SetActive(true);
        messageText.text = message.ToString();
    }

    public void OnChangeNickName(string nickName)
    {
        PlayerPrefsManager.instance.SetNickName(nickName);
        PhotonNetwork.NickName = nickName;
    }
}
