using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NewCardLayoutGroup))]
public abstract class CardCollection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event EventHandler OnMouseEnter;
    public event EventHandler OnMouseExit;

    [SerializeField]
    protected GameObject _cardPrefab = null;

    [SerializeField]
    protected int _maxNumberOfCards = 8;

    protected List<Card> _cards = new List<Card>();
    private Vector3[] _storedPositions;

    private Card _potentialCard;
    private Vector3 _availablePosition;
    
    private ChangeTrackingWrapper<int> _availableCardIndex;
    protected ChangeTrackingWrapper<int> _numberOfCards;

    private NewCardLayoutGroup _newCardLayoutGroup;
    private Canvas _canvas;

    private float moveDuration = 1f;
    
    protected virtual void Awake()
    {
        _newCardLayoutGroup = GetComponent<NewCardLayoutGroup>();
        while (transform.parent != null)
        {
            _canvas = GetComponentInParent<Canvas>();
            if (_canvas != null)
                break;
        }

        _availableCardIndex = new ChangeTrackingWrapper<int>();
        _numberOfCards = new ChangeTrackingWrapper<int>();
    }

    protected virtual void Start()
    {
        _storedPositions = _newCardLayoutGroup.CalculateCardPositions(_numberOfCards.Value);
    }

    public virtual void AddCard(Card card)
    {
        if(_numberOfCards.Value == _maxNumberOfCards)
        {
            Debug.LogWarning("Can't add card to collection, because the collection already has the maximum number of cards");
            return;
        }
        card.SetCurrentCardCollection(this);
        //card.MoveTo(Vector3.zero, moveDuration);
        //_cards.Add(card);
        _numberOfCards.Value++;
        //insert card at available index
        _storedPositions = _newCardLayoutGroup.CalculateCardPositions(_numberOfCards.Value);
        _availableCardIndex.Value = _newCardLayoutGroup.CardIndexClosestToMouse(_storedPositions);
        _cards.Insert(_availableCardIndex.Value, card);
        ResetLayout();
        //move card to available position
        
        /*
        _numberOfCards.Value++;
        card.ChangeCurrentCollection(this);

        _storedPositions = _newCardLayoutGroup.CalculateCardPositions(_numberOfCards.Value);
        _availableCardIndex.Value = _newCardLayoutGroup.CardIndexClosestToMouse(_storedPositions, _numberOfCards.Value);
        _cards.Insert(_availableCardIndex.Value, card);
        
        card.MoveTo(_storedPositions[_storedPositions.Length - 1], moveDuration);
        */
    }

    public void SimpleUpdateLayoutMovingCard(Card card)
    {
        _storedPositions = _newCardLayoutGroup.CalculateCardPositions(_numberOfCards.Value);
        _availableCardIndex.Value = _newCardLayoutGroup.CardIndexClosestToMouse(_storedPositions);
        _availablePosition = _storedPositions[_availableCardIndex.Value];
        MoveUsingDynamicLayout(_storedPositions, _availableCardIndex.Value);
        //card.MoveTo(_availablePosition, moveDuration);
    }
    
    public virtual void RemoveCard(Card card)
    {
        _cards.Remove(card);
        _numberOfCards.Value--;
        ResetLayout();
    }

    public void DraggingCardFromThisCollection(Card card)
    {
        int cardIndex = _cards.IndexOf(card);

    }

    public void DraggingCardFromOtherCollection(Card card)
    {
        //stored positions needs to be called only once
        _storedPositions = _newCardLayoutGroup.CalculateCardPositions(_numberOfCards.Value + 1);
        //availableCardIndex needs to be updated constantly to check if cards need to be moved around
        _availableCardIndex.Value = _newCardLayoutGroup.CardIndexClosestToMouse(_storedPositions);
        //cards only need to be moved around when the availableCardIndex has changed
        ResetLayout();
    }

    public void UpdateLayoutWithMovingCard(Card card)
    {
        if(_numberOfCards.HasChanged)
        {
            _storedPositions = _newCardLayoutGroup.CalculateCardPositions(_numberOfCards.Value);
            _numberOfCards.ResetChangedFlag();
        }
        
        _availableCardIndex.Value = _newCardLayoutGroup.CardIndexClosestToMouse(_storedPositions);
        if(_availableCardIndex.HasChanged)
        {
            _availablePosition = _storedPositions[_availableCardIndex.Value];
            _availableCardIndex.ResetChangedFlag();
        }
        
        if (!_availableCardIndex.HasChanged) { return; }

        //move items to new position
        MoveUsingDynamicLayout(_storedPositions, _availableCardIndex.Value);
        _availableCardIndex.ResetChangedFlag();
    }

    public void ResetLayout()
    {
        Vector3[] positions = _newCardLayoutGroup.CalculateCardPositions(_numberOfCards.Value);
        for (int i = 0; i < _numberOfCards.Value; i++)
        {
            _cards[i].MoveTo(positions[i], moveDuration);
        }
    }

    public void ResetLayoutWithoutDraggedCard(Card card)
    {
        Vector3[] positions = _newCardLayoutGroup.CalculateCardPositions(_numberOfCards.Value);
        for (int i = 0; i < _cards.Count; i++)
        {
            if (_cards[i].Equals(card))
                continue;
            _cards[i].MoveTo(positions[i], moveDuration);
        }
    }

    private void MoveUsingDynamicLayout(Vector3[] positions, int closestElement)
    {
        for (int i = 0; i < _numberOfCards.Value; i++)
        {
            if (i < closestElement)
            {
                _cards[i].MoveTo(positions[i], moveDuration);
            }
            else
            {
                _cards[i].MoveTo(positions[i + 1], moveDuration);
            }
            _cards[i].gameObject.SetActive(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnter(this, EventArgs.Empty);
        //mouse inside area
        if(Input.GetMouseButton(0))
        {
            //check for potential card being dragged
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit(this, EventArgs.Empty);
    }
}