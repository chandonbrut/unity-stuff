using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{

    public Player Player;
    public Transform ForegroundSprite;
    public SpriteRenderer ForegroundRenderer;

    public Color MaxHealthColor = new Color(255 / 255f, 63 / 255f, 63 / 255f);
    public Color MinHealthColor = new Color(64 / 255f, 137 / 255f, 255 / 255f);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var healthPercent = Player.Health / Player.MaxHealth;

        ForegroundSprite.localScale = new Vector3(healthPercent, 1, 1);
        ForegroundRenderer.color = Color.Lerp(MaxHealthColor, MinHealthColor, healthPercent);

    }
}
