using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NewCardLayoutGroup), typeof(CardDisplayer))]
public abstract class CardCollection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject _cardPrefab = null;

    [SerializeField]
    private int _maxNumberOfCards = 8;

    [SerializeField]
    private float _spacing = 0f;

    private int _numberOfCards = 0;

    private List<GameObject> _cardGOs = new List<GameObject>();
    private Vector3[] _cardPositions;
    
    private Card _potentialCard;
    private Vector3 _availablePosition;
    private ChangeTrackingWrapper<int> _availableCardIndex/* = new ChangeTrackingWrapper<int>(0, _cardDisplayer.SetPositionWithMovingCard())*/;

    private CardLayoutGroup _cardLayoutGroup;
    private CardDisplayer _cardDisplayer;

    private Canvas _canvas;

    private float moveDuration = 1f;
    
    protected virtual void Awake()
    {
        _cardLayoutGroup = new CardLayoutGroup(_spacing);
        _cardDisplayer = new CardDisplayer(transform, _cardPrefab, Camera.main, _maxNumberOfCards);

        while (transform.parent != null)
        {
            _canvas = GetComponentInParent<Canvas>();
            if (_canvas != null)
                break;
        }
    }

    protected virtual void Start()
    {
        _cardDisplayer.SetCanvasScale(_canvas.scaleFactor);
        _cardDisplayer.SetDefaultPositions(_cardLayoutGroup, _numberOfCards);
    }

    public virtual void AddCard(GameObject cardGO)
    {
        Card card = cardGO.GetComponent<Card>();
        card.MoveTo(_availablePosition, moveDuration);
        card.ChangeCurrentCollection(this);
        _cardGOs.Add(cardGO);
    }
    
    public virtual void RemoveCard(GameObject cardGO)
    {
        _cardGOs.Remove(cardGO);
    }

    public void StartDraggingCard(GameObject cardGO)
    {
        //get card element in array
        int cardIndex = GetCardIndex(cardGO);
        //set card positions based on cardIndexClosestToMouse, but excluse the dragged card

    }

    private int GetCardIndex(GameObject cardGO)
    {
        if(!_cardGOs.Contains(cardGO)) { return -1; }

        for (int i = 0; i < _cardGOs.Count; i++)
        {
            if (_cardGOs[i].Equals(cardGO))
                return i;
        }
        return -1;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //mouse inside area
        if(Input.GetMouseButton(0))
        {
            //check for potential card being dragged
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_cardPositions.Length != _numberOfCards + 1)
            _cardPositions = _cardLayoutGroup.GetCardPositions(_numberOfCards + 1);
        _availableCardIndex.Value = _cardDisplayer.CardIndexClosestToMouse(_cardPositions, _numberOfCards);
        _cardDisplayer.SetPositionWithExtraCard(_cardLayoutGroup, _numberOfCards);
    }
}