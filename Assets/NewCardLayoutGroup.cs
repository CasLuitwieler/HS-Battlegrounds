using UnityEngine;

public class NewCardLayoutGroup : MonoBehaviour
{
    [SerializeField]
    protected float _horizontalSpacing = 150, _verticalSpacing = 0;

    public Vector3[] CalculateCardPositions(int numberOfCards)
    {
        Vector3[] positions = new Vector3[numberOfCards];
        
        float centerIndex = GetArrayCenterIndex(numberOfCards);

        for (int i = 0; i < numberOfCards; i++)
        {
            float xPosition = (centerIndex - i) * _horizontalSpacing;
            float yPosition = (centerIndex - i) * _verticalSpacing;
            positions[i] = new Vector3(xPosition, yPosition, 0f);
        }

        return positions;
    }

    public Vector3[] CalculatePositionWithMovingCard(int numberOfCards)
    {
        Vector3[] positions = new Vector3[numberOfCards];



        return positions;
    }

    protected float GetArrayCenterIndex(int numberOfCards)
    {
        float centerIndex;
        bool numberOfCardsIsEven = numberOfCards % 2 == 0 ? true : false;

        if (numberOfCardsIsEven)
        {
            int lastElement = numberOfCards - 1;
            centerIndex = lastElement / 2f;
        }
        else
        {
            centerIndex = Mathf.FloorToInt(numberOfCards / 2f);
        }
        return centerIndex;
    }
}
