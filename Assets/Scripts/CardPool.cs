using System.Collections.Generic;
using UnityEngine;

public class CardPool : MonoBehaviour
{
    [SerializeField]
    private CardData[] _tierOneCards = null, _tierTwoCards = null, _tierThreeCards = null;

    private Dictionary<int, CardData[]> _cards = new Dictionary<int, CardData[]>();

    private int getNumberOfCards(int tier) => _cards[tier].Length;

    public void Init()
    {
        //TODO: have dictionary available in inspector or get cards from asset folder
        _cards[1] = _tierOneCards;
        _cards[2] = _tierTwoCards;
        _cards[3] = _tierThreeCards;
    }

    public List<CardData> GetRandomCards(int tier, int numberOfCards)
    {
        List<CardData> randomCards = new List<CardData>();

        for (int i = 0; i < numberOfCards; i++)
        {
            int randomNumber = Random.Range(0, getNumberOfCards(tier));
            randomCards.Add(_cards[tier][randomNumber]);
        }

        return randomCards;
    }
}
