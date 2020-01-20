using System.Collections.Generic;
using UnityEngine;

public class NewCardLayoutGroup : MonoBehaviour
{
    [SerializeField]
    protected float _horizontalSpacing = 150, _verticalSpacing = 0;

    private float _halfWidth = 0, _halfHeight = 0, _scaleFactor = 0;

    private Vector3[] _cardPositions;
    private ChangeTrackingWrapper<int> _numberOfCards;

    private Camera _cam;
    private Canvas _canvas;

    private void Awake()
    {
        _cam = Camera.main;
        while (transform.parent != null)
        {
            _canvas = GetComponentInParent<Canvas>();
            if (_canvas != null)
                break;
        }
    }

    private void Start()
    {
        _halfWidth = Screen.width / 2f;
        _halfHeight = Screen.height / 2f;
        _scaleFactor = _canvas.scaleFactor;

        _numberOfCards = new ChangeTrackingWrapper<int>();
    }

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

    public Vector3[] GetPositionOnChange(int numberOfCards)
    {
        _numberOfCards.Value = numberOfCards;
        if (!_numberOfCards.HasChanged) { return _cardPositions; }

        _numberOfCards.ResetChangedFlag();
        _cardPositions = CalculateCardPositions(numberOfCards);
        return _cardPositions;
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

    public int CardIndexClosestToMouse(Vector3[] cardPositions)
    {
        Vector3 rawMousePos = Input.mousePosition;
        Vector3 mousePos = new Vector3((rawMousePos.x - _halfWidth) / _scaleFactor, 0f, 0f);

        //TODO: start at -1, with exception when the cardPositions array is empty, then return 0
        int closestElement = 0;
        float shortestDistance = Mathf.Infinity;

        for (int i = 0; i < cardPositions.Length; i++)
        {
            float distance = Mathf.Abs(mousePos.x - cardPositions[i].x);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestElement = i;
            }
        }

        return closestElement;
    }
}
