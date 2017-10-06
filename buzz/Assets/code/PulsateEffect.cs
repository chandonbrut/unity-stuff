using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PulsateEffect : MonoBehaviour
{
    [Range(0, 100)]
    public float RescaleRatio;

    private float _delta;

    private float _originalScale;
    
    private int _direction = 1;
    private float _scaleUpperLimit;
    private float _scaleLowerLimit;


    // Use this for initialization
    void Start()
    {
        _originalScale = gameObject.transform.localScale.x;
        _delta = 0.1f ;
        _scaleUpperLimit = _originalScale + ( RescaleRatio / 100f );
        _scaleLowerLimit = _originalScale - ( RescaleRatio / 100f );

    }

    // Update is called once per frame
    void Update()
    {
        var currentScale = (_direction == 1) ? new Vector3(gameObject.transform.localScale.x + _delta, gameObject.transform.localScale.x + _delta, 0) : new Vector3(gameObject.transform.localScale.x - _delta, gameObject.transform.localScale.x - _delta, 0);

        if (currentScale.x >= _scaleUpperLimit)
        {
            currentScale.x = _scaleUpperLimit;
            currentScale.y = _scaleUpperLimit;
            _direction = -1;        
        }

        if (currentScale.x <= _scaleLowerLimit)
        {
            currentScale.x = _scaleLowerLimit;
            currentScale.y = _scaleLowerLimit;
            _direction = 1;            
        }

        gameObject.transform.localScale = currentScale;

    }
    
}
