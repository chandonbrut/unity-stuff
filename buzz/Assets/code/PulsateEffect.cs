using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PulsateEffect : MonoBehaviour
{
    [Range(0, 100)]
    public float RescaleRatio;

    private float _delta;

    private Vector3 _originalScale;
    
    private int _direction = 1;
    private float _scaleUpperLimit;
    private float _scaleLowerLimit;


    // Use this for initialization
    void Start()
    {
        _originalScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        var scale = Mathf.Abs((Time.time % 4) - 2) + 1;
        var currentScale = new Vector3(scale * (RescaleRatio/100f), scale * (RescaleRatio/100f), 0);
        gameObject.transform.localScale = _originalScale + currentScale;
        
    }
    
}
