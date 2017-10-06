using UnityEngine;
using System.Collections;

public class CenteredTextPositioner : IFloatingTextPositioner
{

    private readonly float _speed;
    private float _textPosition;

    public CenteredTextPositioner(float speed)
    {
        _speed = speed;
    }

    public bool GetPosition(ref Vector2 position, GUIContent content, Vector2 contentSize)
    {
        _textPosition += Time.deltaTime * _speed;
        if (_textPosition > 1) return false;
        position = new Vector2(Screen.width / 2f - contentSize.x / 2f, Mathf.Lerp(Screen.height / 2f + contentSize.y, 0, _textPosition ));

        return true;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
