using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CardUI))]
public class Card : MonoBehaviour
{
    public CardCollection CurrentCardCollection { get; private set; }
    public CardData CardData { get; private set; }
    
    private CardUI _cardUI;

    private void Awake()
    {
        _cardUI = GetComponent<CardUI>();
    }

    public void SetCurrentCardCollection(CardCollection cardCollection)
    {
        CurrentCardCollection = cardCollection;
        transform.SetParent(cardCollection.transform);
    }

    public void SetDataAndUI(CardData data)
    {
        CardData = data;
        _cardUI.AssignUI(data);
    }
    
    public void MoveTo(Vector3 targetPosition, float duration)
    {
        //DOTween.Clear();
        transform.DOLocalMove(targetPosition, duration);
    }
}
