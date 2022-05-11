using Photon.Pun;
using UnityEngine;

public class AmogusPlayer : MonoBehaviour
{
    [SerializeField]
    public float Speed;
    [SerializeField]
    public bool CanMove;
    [SerializeField]
    public bool CanDoMinigames;
    [SerializeField]
    public bool CanKill;
    [SerializeField]
    public int ActorNumber;

    private Rigidbody2D rigidbody2d;
    
    private Vector2 Movement;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            Movement.x = Input.GetAxisRaw("Horizontal");
            Movement.y = Input.GetAxisRaw("Vertical");

            rigidbody2d.MovePosition(rigidbody2d.position + Movement * Speed * Time.fixedDeltaTime);

            GameManager.Instance.MovePlayer(rigidbody2d.transform.position);
        }
    }

    private void Update()
    {
        if (CanDoMinigames)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                for (int i = 0; i < MinigameManager.Instance.MyMinigamePlaces.Count; i++)
                {
                    if (Vector3.Distance(MinigameManager.Instance.MyMinigamePlaces[i].transform.position, transform.position) <= 3F)
                    {
                        MinigameManager.Instance.StartMinigame(MinigameManager.Instance.MyMinigamePlaces[i]);
                        break;
                    }
                }
            }
        }

        if (CanKill)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                foreach (var player in GameManager.Instance.Players.Values)
                {
                    if (Vector3.Distance(player.transform.position, transform.position) <= 2F)
                    {
                        if (player.ActorNumber != ActorNumber)
                        {
                            GameManager.Instance.KillPlayer(player.ActorNumber);
                            break;
                        }
                    }
                }
            }
        }
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}