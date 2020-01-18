using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private Canvas _canvas;

    void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        transform.localPosition = new Vector3(0f, 105f, 0f);
    }
    
}
