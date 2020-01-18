using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CardUI))]
public class Card : MonoBehaviour
{
    public CardCollection CurrentCardCollection { get; private set; }
    public void ChangeCurrentCollection(CardCollection cardCollection) => CurrentCardCollection = cardCollection;

    private CardData _cardData;
    private CardUI _cardUI;

    private void Awake()
    {
        _cardUI = GetComponent<CardUI>();
    }

    public void SetDataAndUI(CardData data)
    {
        _cardData = data;
        _cardUI.AssignUI(data);
    }

    public void MoveTo(Vector3 targetPosition, float duration)
    {
        transform.DOLocalMove(targetPosition, duration);
    }
}
