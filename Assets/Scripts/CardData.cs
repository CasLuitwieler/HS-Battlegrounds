using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardData")]
public class CardData : ScriptableObject
{
    public string Name = "New card name";
    public string Description = "New card description";
    public int Attack = 0;
    public int Defense = 0;
    public Sprite Art;
    public List<CardEffect> Effects;
}


public class CardEffect
{
    //moment to invoke

    //invoke behaviour

    //
}
