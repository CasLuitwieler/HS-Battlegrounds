using System;
using UnityEngine;

public class CardCollectionManager : MonoBehaviour
{
    private static CardCollection _draggedCardOriginalCollection;
    private static CardCollection _pointerOverCollection;

    private static Card _draggedCard;

    private static bool _isDragging;

    private CardCollection[] _cardCollections;
    
    private void Awake()
    {
        _cardCollections = GetComponentsInChildren<CardCollection>();
        foreach(CardCollection cardCollection in _cardCollections)
        {
            cardCollection.OnMouseEnter += PointerEnterCollection;
            cardCollection.OnMouseExit += PointerExitCollection;
        }
    }
    
    private void FixedUpdate()
    {
        if(_isDragging)
        {
            if(_pointerOverCollection == _draggedCardOriginalCollection)
            {
                Debug.Log("default collection");
                _pointerOverCollection.SimpleUpdateLayoutMovingCard(_draggedCard);
            }
            else if(_pointerOverCollection == null)
            {
                Debug.Log("pointer over nothing, default collection selected");
                _draggedCardOriginalCollection.SimpleUpdateLayoutMovingCard(_draggedCard);
            }
            else
            {
                Debug.Log("pointer over different collection");
            }
        }
    }

    public void PointerEnterCollection(object obj, EventArgs e)
    {
        CardCollection cardCollection = obj as CardCollection;
        _pointerOverCollection = cardCollection;

        if(_isDragging)
        {
            //move cards from original collection back
            if(!_pointerOverCollection.Equals(_draggedCardOriginalCollection))
            {
                _draggedCardOriginalCollection.ResetLayoutWithoutDraggedCard(_draggedCard);
            }
        }
    }

    public void PointerExitCollection(object obj, EventArgs e)
    {
        if(_isDragging && !_pointerOverCollection.Equals(_draggedCardOriginalCollection))
        {
            //set cards back to default position
            _pointerOverCollection.ResetLayout();
        }

        _pointerOverCollection = null;
    }

    public static void StartDrag(Card card)
    {
        _draggedCardOriginalCollection = card.CurrentCardCollection;
        _isDragging = true;
        _draggedCard = card;
    }

    public static void StopDrag(Card card)
    {
        if(_pointerOverCollection == null)
        {
            //move card back to original collection and position
            //_draggedCard.MoveTo(_draggedCardOriginalCollection.GetAvailablePosition());
            _draggedCardOriginalCollection.ResetLayout();
        }
        else
        {
            //transfer card to new card collecion
            _draggedCardOriginalCollection.RemoveCard(card);
            _pointerOverCollection.AddCard(card);
            //move card to available position
        }
        _draggedCardOriginalCollection = null;

        _isDragging = false;
        _draggedCard = null;
    }
}
