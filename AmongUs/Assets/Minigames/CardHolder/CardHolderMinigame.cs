using UnityEngine;
using UnityEngine.UI;

public class CardHolderMinigame : MonoBehaviour
{
    [SerializeField]
    public RectTransform CardRectTransform;
    [SerializeField]
    public Image CardHoldeImage;
    [SerializeField]
    public Sprite SuccessCardHolder;
    [SerializeField]
    public Sprite DefaultCardHolder;
    [SerializeField]
    public Sprite FailCardHolder;

    private Vector2 StartPosition;
    private Vector2 CurrentPosition;
    private bool IsDragging = false;
    private bool CanDragging = true;
    private float DraggingTime = 0F;

    private void Start()
    {
        StartPosition = CardRectTransform.position;
    }

    public void BeginDragEvent()
    {
        if (!CanDragging) return;

        IsDragging = true;
        CurrentPosition = StartPosition;
        DraggingTime = 0F;
    }

    private void FixedUpdate()
    {
        if (CanDragging && IsDragging) DraggingTime += Time.fixedDeltaTime;
    }

    public void DragEvent()
    {
        if (!CanDragging) return;

        CurrentPosition.x = Input.mousePosition.x;
        CurrentPosition.y = StartPosition.y;

        CardRectTransform.position = CurrentPosition;
    }    

    public void EndDragEvent()
    {
        if (!CanDragging) return;

        IsDragging = false;
        print($"Drag time = {DraggingTime}");

        bool cond1 = 222F - 10F < CardRectTransform.localPosition.x && 222F + 10F > CardRectTransform.localPosition.x;
        bool cond2 = 1F - 0.2F < DraggingTime && 1F + 0.2F > DraggingTime;

        if (cond1 && cond2)
        {
            CardHoldeImage.sprite = SuccessCardHolder;
            CanDragging = false;
            //Задание выполнено
            GameManager.Instance.DoneMinigame();
        }
        else
        {
            CardHoldeImage.sprite = FailCardHolder;
            CardRectTransform.position = StartPosition;
        } 
    }

    public void ShowAndReset()
    {
        CanDragging = true;
        CardRectTransform.position = StartPosition;
        CardHoldeImage.sprite = DefaultCardHolder;
    }
}
