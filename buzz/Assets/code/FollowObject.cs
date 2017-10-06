using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour
{

    public Vector2 Offset;
    public Transform Following;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Following.transform.position + (Vector3)Offset;
    }
}
