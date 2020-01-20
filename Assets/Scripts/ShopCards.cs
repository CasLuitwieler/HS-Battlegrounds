using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardPool))]
public class ShopCards : CardCollection
{
    [SerializeField]
    private int _startCards = 3, _startTier = 1;

    private CardPool _cardPool;

    protected override void Awake()
    {
        base.Awake();

        _cardPool = GetComponent<CardPool>();
        _cardPool.Init();
        RefreshShop();
    }

    public override void AddCard(Card card)
    {
        if(card.CurrentCardCollection == null)
        {
            //TODO: have seperate AddCard for init and selling cards
            card.transform.localPosition = Vector3.zero;
        }
        card.SetCurrentCardCollection(this);
        _cards.Add(card);
        _numberOfCards.Value++;
        ResetLayout();
    }

    public override void RemoveCard(Card card)
    {
        //subtract gold
        base.RemoveCard(card);
    }

    public void RefreshShop()
    {
        //TODO: change _startCards to amount based on shop tier
        //TODO: clear old cards
        List<CardData> randomCards = _cardPool.GetRandomCards(_startTier, _startCards);
        for (int i = 0; i < randomCards.Count; i++)
        {
            GameObject cardGO = Instantiate(_cardPrefab, transform);
            Card card = cardGO.GetComponent<Card>();
            card.SetDataAndUI(randomCards[i]);
            AddCard(card);
        }
    }
}