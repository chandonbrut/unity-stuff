using UnityEngine;
using System.Collections;

public class AwardPoints : MonoBehaviour
{
    public int PointsAwarded;

    public void AwardPointsCo()
    {

        GameManager.Instance.AddPoints(PointsAwarded);
        gameObject.SetActive(false);
        FloatingText.Show(string.Format("+{0}", PointsAwarded), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 50f));
    }
    
}
