using UnityEngine;

public class PlayerCards : CardCollection
{
    public PlayerCards()
    {
        
    }

    public override void AddCard(GameObject cardGO)
    {
        //check if enough gold
        //check for 3 of the same cards
        base.AddCard(cardGO); //add card to collection
    }
}