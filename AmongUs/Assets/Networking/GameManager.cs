using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dictionary<int, AmogusPlayer> Players = new Dictionary<int, AmogusPlayer>();

    private PhotonView photonView;
    private int MinigamesCount;
    public static GameManager Instance;

    private void Awake()
    {
        //Singleton
        Instance = this;
        photonView = GetComponent<PhotonView>();

        if (PhotonNetwork.IsMasterClient)
        {
            //Это сервер
            gameObject.AddComponent<HostManager>();
        }
        else
        {
            //Это клиент
            gameObject.AddComponent<ClientManager>();
        }
    }

    private void Start()
    {
        int ImposterID = PhotonNetwork.PlayerList[Random.Range(0, PhotonNetwork.PlayerList.Length)].ActorNumber;
        MinigamesCount = (PhotonNetwork.PlayerList.Length - 1) * 3;

        foreach (var player in PhotonNetwork.PlayerList)
        {
            AmogusPlayer amogusPlayer;

            if (player.ActorNumber.Equals(PhotonNetwork.LocalPlayer.ActorNumber)) amogusPlayer = PlayerController.Instance.SpawnMyPlayer(player.NickName);
            else amogusPlayer = PlayerController.Instance.SpawnOtherPlayer(player.NickName);

            if (player.ActorNumber.Equals(ImposterID)) photonView.RPC("RpcSetStatus", player, 0);
            else photonView.RPC("RpcSetStatus", player, 1);

            amogusPlayer.ActorNumber = player.ActorNumber;
            Players.Add(player.ActorNumber, amogusPlayer);
        }
    }

    public void MovePlayer(Vector3 newPosition)
    {
        photonView.RPC("RpcMovePlayer", RpcTarget.Others, PhotonNetwork.LocalPlayer.ActorNumber, newPosition);
    }

    [PunRPC]
    private void RpcMovePlayer(int actorNumber, Vector3 newPosition)
    {
        Players[actorNumber].SetPosition(newPosition);
    }

    public void DoneMinigame()
    {
        MinigameManager.Instance.ResetMinigame();
        photonView.RPC("RpcDoneMinigame", RpcTarget.MasterClient);
    }

    [PunRPC]
    private void RpcDoneMinigame()
    {
        MinigamesCount--;
        if (MinigamesCount <= 0)
        {
            Debug.Log("Crewmates are win!");
        }
    }

    public void KillPlayer(int actorNumber)
    {
        photonView.RPC("RpcKillPlayer", RpcTarget.MasterClient, actorNumber);
    }

    [PunRPC]
    public void RpcKillPlayer(int actorNumber)
    {
        PhotonNetwork.Instantiate("DeadBody", Players[actorNumber].transform.position, Quaternion.identity);
        photonView.RPC("RpcInitDeadPlayer", RpcTarget.All, actorNumber);
    }

    [PunRPC]
    public void RpcInitDeadPlayer(int actorNumber)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == actorNumber) PlayerController.Instance.InitDeath();
        else Players[actorNumber].gameObject.SetActive(false);
    }
}
