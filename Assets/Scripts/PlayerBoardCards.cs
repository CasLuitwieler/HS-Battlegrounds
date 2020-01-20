using UnityEngine;

public class PlayerBoardCards : CardCollection
{
    public PlayerBoardCards()
    {
        
    }

    public override void AddCard(Card card)
    {
        //check if enough gold
        //check for 3 of the same cards
        base.AddCard(card); //add card to collection
    }
}