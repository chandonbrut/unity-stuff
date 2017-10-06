using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour
{

    private List<IPlayerRespawnListener> _listeners;

    public void PlayerHitCheckpoint()
    {
        StartCoroutine(PlayerHitCheckpointCo(LevelManager.Instance.CurrentTimeBonus));
    }
    private IEnumerator PlayerHitCheckpointCo(int bonus)
    {

        FloatingText.Show(string.Format("Checkpoint!"), "CheckpointText", new CenteredTextPositioner(0.2f));

        yield return new WaitForSeconds(0.5f);

        FloatingText.Show(string.Format("+{0} time bonus!", bonus), "CheckpointText", new CenteredTextPositioner(0.2f));


        yield break;
    }
    public void PlayerLeftCheckpoint()
    {

    }
    public void SpawnPlayer(Player player)
    {
        player.RespawnAt(transform);
        foreach(var listener in _listeners)
        {
            listener.OnPlayerRespawnInThisCheckpoint(this, player);
        }
    }
    public void AssignObjectToCheckpoint(IPlayerRespawnListener listener)
    {
        _listeners.Add(listener);
    }
    void Awake()
    {
        _listeners = new List<IPlayerRespawnListener>();
    }
    void Update()
    {

    }
}