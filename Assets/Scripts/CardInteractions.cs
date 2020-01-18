using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Card), typeof(CanvasGroup))]
public class CardInteractions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Card _card;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _card = GetComponent<Card>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //glow, grow, move to front, move others away
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //stop glow, shrink, move back in line
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        _card.CurrentCardCollection.StartDraggingCard(gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //follow mouse
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
    }
}
