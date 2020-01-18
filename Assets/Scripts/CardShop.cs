using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CardPool))]
public class CardShop : MonoBehaviour
{
    public CardCollection ShopCards { get; private set; }

    [SerializeField]
    private GameObject _cardPrefab;
    
    [SerializeField]
    private int _numberOfCards = 3, _maxNumberOfCards = 8;
    private int _previousNumberOfCards = 0;

    private int _playerTier = 0;
    
    private CardPool _cardPool;

    private void Awake()
    {
        _cardPool = GetComponent<CardPool>();
        ShopCards = new ShopCards();
    }

    private void Update()
    {
        /*
        if (_numberOfCards != _previousNumberOfCards)
        {
            _cardDisplayer.SetDefaultCardPosition(_cardLayoutGroup, _numberOfCards);
        }
        */
    }
    
    public void RefreshShop()
    {
        List<CardData> _randomCards = _cardPool.GetRandomCards(_playerTier);

        foreach(CardData cardData in _randomCards)
        {
            //ShopCards.AddCard(cardData);
        }

        //ShopCards.ShowCards();

        //OnDisplayCards(this, new CardCollectionData(_collectionID, _newCards));
    }
}
