using UnityEngine;

public class CardLayoutGroup
{
    private float _spacing;

    public CardLayoutGroup(float spacing)
    {
        _spacing = spacing;
    }

    public Vector3[] GetCardPositions(int numberOfCards)
    {
        Vector3[] cardTransforms;

        bool evenNumberOfCards = numberOfCards % 2 == 0 ? true : false;

        if(evenNumberOfCards)
        {
            CalculatePositionsEven(numberOfCards, out cardTransforms);
        }
        else
        {
            CalculatePositionsUneven(numberOfCards, out cardTransforms);
        }

        return cardTransforms;
    }

    private void CalculatePositionsEven(int numberOfCards, out Vector3[] cardTransforms)
    {
        cardTransforms = new Vector3[numberOfCards];

        int lastElement = numberOfCards - 1;
        float centerNumber = lastElement / 2f;        
        for (int i = 0; i < numberOfCards; i++)
        {
            float x = (i - centerNumber) * _spacing;
            cardTransforms[i] = new Vector3(x, 0f, 0f);
        }
    }

    private void CalculatePositionsUneven(int numberOfCards, out Vector3[] cardTransforms)
    {
        cardTransforms = new Vector3[numberOfCards];

        int centerElement = Mathf.FloorToInt(numberOfCards / 2f);
        for (int i = 0; i < numberOfCards; i++)
        {
            float x = (i - centerElement) * _spacing;
            cardTransforms[i] = new Vector3(x, 0f, 0f);
        }
    }
}
