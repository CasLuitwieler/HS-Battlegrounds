using System;
using System.Collections.Generic;
using UnityEngine;

public class CardDisplayer
{
    private GameObject[] _cardObjects;
    private Vector3[] _cardPositions;

    private float _halfWidth = 0, _halfHeight = 0, _scaleFactor = 0;

    private int _previousClosestElement = -1;
    
    private Camera _cam;

    public CardDisplayer(Transform owner, GameObject cardPrefab, Camera cam, int maxNumberOfCards)
    {
        _cam = cam;
        _cardObjects = new GameObject[maxNumberOfCards];
        for (int i = 0; i < maxNumberOfCards; i++)
        {
            _cardObjects[i] = GameObject.Instantiate(cardPrefab, owner);
        }
        _halfWidth = Screen.width / 2f;
        _halfHeight = Screen.height / 2f;
    }

    public void SetCanvasScale(float scaleFactor) => _scaleFactor = scaleFactor;

    public void SetDefaultPositions(CardLayoutGroup layoutGroup, int numberOfCards)
    {
        _cardPositions = layoutGroup.GetCardPositions(numberOfCards);

        for (int i = 0; i < _cardPositions.Length; i++)
        {
            _cardObjects[i].transform.localPosition = _cardPositions[i];
            _cardObjects[i].SetActive(true);
        }
        HideUnusedCardObjects(numberOfCards);
    }

    public void SetPositionWithExtraCard(CardLayoutGroup layoutGroup, int numberOfCards)
    {
        if(_cardPositions.Length != numberOfCards + 1)
            _cardPositions = layoutGroup.GetCardPositions(numberOfCards + 1);

        int closestElement = CardIndexClosestToMouse(_cardPositions, numberOfCards + 1);

        if (closestElement == _previousClosestElement) { return; }

        _previousClosestElement = closestElement;

        SetPositionWithMovingCard(closestElement);

        Vector3 availablePosition = _cardPositions[closestElement];
        int availableElement = closestElement;

        //HideUnusedCardObjects(numberOfCards);
    }

    public void SetPositionWithMovingCard(int closestElement)
    {
        for (int i = 0; i < _cardPositions.Length - 1; i++)
        {
            if (i < closestElement)
            {
                _cardObjects[i].transform.localPosition = _cardPositions[i];
            }
            else
            {
                _cardObjects[i].transform.localPosition = _cardPositions[i + 1];
            }
            _cardObjects[i].SetActive(true);
        }
    }

    public int CardIndexClosestToMouse(Vector3[] cardPositions, int numberOfCards)
    {
        Vector3 rawMousePos = Input.mousePosition;
        Vector3 mousePos = new Vector3((rawMousePos.x - _halfWidth) / _scaleFactor, 0f, 0f);

        int closestElement = -1;
        float shortestDistance = Mathf.Infinity;

        for (int i = 0; i < _cardPositions.Length; i++)
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

    private void SetCardData(List<CardData> cardData)
    {
        for (int i = 0; i < cardData.Count; i++)
        {
            _cardObjects[i].GetComponent<Card>().SetDataAndUI(cardData[i]);
        }
    }

    private void HideUnusedCardObjects(int numberOfCards)
    {
        for (int i = numberOfCards; i < _cardObjects.Length; i++)
        {
            _cardObjects[i].SetActive(false);
        }
    }

    public void HideCards(object o, EventArgs e)
    {
        HideUnusedCardObjects(0);
    }
}
