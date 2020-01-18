using UnityEngine;

public class ShopCards : CardCollection
{
    public ShopCards()
    {
        
    }

    public override void AddCard(GameObject cardGO)
    {

        base.AddCard(cardGO);
    }

    public override void RemoveCard(GameObject cardGO)
    {
        //subtract gold
        base.RemoveCard(cardGO);
    }
}