using System.Collections.Generic;
using UnityEngine;

public class CardPool : MonoBehaviour
{
    private Dictionary<int, CardData[]> _cards = new Dictionary<int, CardData[]>();

    private int getNumberOfCards(int tier) => _cards[tier].Length;

    public List<CardData> GetRandomCards(int tier)
    {
        List<CardData> randomCards = new List<CardData>(tier);

        for (int i = 0; i < tier; i++)
        {
            int randomNumber = Random.Range(0, getNumberOfCards(tier));
            randomCards.Add(_cards[tier][randomNumber]);
        }

        return randomCards;
    }
}
