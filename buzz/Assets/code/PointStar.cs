using UnityEngine;
using System.Collections;

public class PointStar : MonoBehaviour, IPlayerRespawnListener
{

    public GameObject Effect;

    public void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player)
    {
        gameObject.SetActive(true);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() == null)
            return;

        Instantiate(Effect, transform.position, transform.rotation);
        gameObject.SetActive(false);

        var awarder = gameObject.GetComponent<AwardPoints>();
        if (awarder != null) awarder.AwardPointsCo();        

    }
    
}
