using UnityEngine;
using System.Collections;

public class RotateEffect : MonoBehaviour
{

    [Range(0,180)]
    public float Revolutions;

    public bool Clockwise;
    
    void Update()
    {
        var orientation = Clockwise ? 1 : -1;
        gameObject.transform.Rotate(new Vector3(0, 0, Time.deltaTime * Revolutions*orientation));
    }
}