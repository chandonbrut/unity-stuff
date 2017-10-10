using UnityEngine;
using System.Collections;

public class FinishLevel : MonoBehaviour
{

    public string NextLevelName;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() == null) return;

        LevelManager.Instance.GotoNextLevel(NextLevelName);
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
