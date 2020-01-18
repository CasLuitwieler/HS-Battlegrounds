using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField]
    private Text _cardName = null, _cardDescription = null, _cardAttack = null, _cardDefense = null;

    [SerializeField]
    private Image _cardArt = null;

    public void AssignUI(CardData cardData)
    {
        _cardName.text = cardData.Name;
        _cardDescription.text = cardData.Description;
        _cardAttack.text = cardData.Attack.ToString();
        _cardDefense.text = cardData.Defense.ToString();
        _cardArt.sprite = cardData.Art;
    }
}
