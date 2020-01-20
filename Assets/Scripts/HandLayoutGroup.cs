using UnityEngine;

public class HandLayoutGroup : NewCardLayoutGroup
{
    [SerializeField]
    private float _tilt = 7.5f;

    public float[] GetTilt(int numberOfCards)
    {
        float[] tiltValues = new float[numberOfCards];
        float centerIndex = GetArrayCenterIndex(numberOfCards);

        for (int i = 0; i < numberOfCards; i++)
        {
            tiltValues[i] = (centerIndex - i) * _tilt;
        }

        return tiltValues;
    }
}
