using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> AvailableMinigamePlaces;
    [SerializeField]
    public CardHolderMinigame CardHolderMinigame;

    public List<GameObject> MyMinigamePlaces = new List<GameObject>();
    public GameObject CurrentMinigame;

    private const int MINIGAME_COUNT = 3;

    public static MinigameManager Instance;

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < MINIGAME_COUNT; i++)
        {
            int index = Random.Range(0, AvailableMinigamePlaces.Count);
            MyMinigamePlaces.Add(AvailableMinigamePlaces[index]);
            AvailableMinigamePlaces.RemoveAt(index);
        }

        for (int i = 0; i < MyMinigamePlaces.Count; i++)
        {
            MyMinigamePlaces[i].GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

    public void StartMinigame(GameObject gameObject)
    {
        CurrentMinigame = gameObject;
        CardHolderMinigame.gameObject.SetActive(true);
        CardHolderMinigame.ShowAndReset();
    }

    public void ResetMinigame()
    {
        CurrentMinigame.GetComponent<SpriteRenderer>().color = Color.white;
        MyMinigamePlaces.Remove(CurrentMinigame);
        CurrentMinigame = null;
    }
}
