using UnityEngine;

public class CollisionZone : MonoBehaviour
{
    [SerializeField]
    public AmogusPlayer Player;

    private void Update()
    {
        transform.position = Player.transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.tag == "MinigameObject")
        {
            if (Player.CanDoMinigames)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    MinigameManager.Instance.StartMinigame(collision.gameObject);
                }
            }
        }

        print(collision.gameObject.name);
    }
}
