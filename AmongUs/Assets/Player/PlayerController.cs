using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerPrefab;
    [SerializeField]
    public TMP_Text StatusText;

    private AmogusPlayer CurrentPlayer;

    public static PlayerController Instance;

    private void Awake()
    {
        //Singleton
        Instance = this;
    }

    public AmogusPlayer SpawnMyPlayer(string nickname)
    {
        GameObject playerObject = Instantiate(PlayerPrefab);
        playerObject.name = nickname;

        CurrentPlayer = playerObject.GetComponent<AmogusPlayer>();

        playerObject.GetComponent<AmogusPlayer>().CanMove = true;

        return playerObject.GetComponent<AmogusPlayer>();
    }

    public AmogusPlayer SpawnOtherPlayer(string nickname)
    {
        GameObject playerObject = Instantiate(PlayerPrefab);
        playerObject.name = nickname;

        playerObject.GetComponent<Rigidbody2D>().simulated = false;
        playerObject.GetComponent<PolygonCollider2D>().enabled = false;

        return playerObject.GetComponent<AmogusPlayer>();
    }

    [PunRPC]
    public void RpcSetStatus(int status)
    {
        if (status == 0)
        {
            //Imposter
            CurrentPlayer.CanDoMinigames = false;
            CurrentPlayer.CanKill = true;
            StatusText.text = "Imposter";
        }
        else
        {
            //Crewmate
            CurrentPlayer.CanDoMinigames = true;
            CurrentPlayer.CanKill = false;
            StatusText.text = "Crewmate";
            //Выполнять квесты
            //Нажимать кнопку
        }
    }

    public void InitDeath()
    {
        CurrentPlayer.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
