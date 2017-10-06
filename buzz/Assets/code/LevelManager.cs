using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { get; private set; }
    public Player Player { get; private set; }
    public CameraController2D Camera { get; private set; }
    public TimeSpan RunningTime {  get { return DateTime.UtcNow - _started;  } }

    public int CurrentTimeBonus
    {
        get
        {
            var secondDifference = (int)(BonusCutoffSeconds - RunningTime.TotalSeconds);
            return Mathf.Max(0, secondDifference) * BonusSecondMultiplier;
        }
    }

    private List<Checkpoint> _checkpoints;
    private int _currentCheckpointIndex;
    private DateTime _started;
    private int _savedPoints;


    public Checkpoint DebugSpawn;
    public int BonusCutoffSeconds;
    public int BonusSecondMultiplier;


    public void Awake()
    {
        Instance = this;  
    }

    public void KillPlayer()
    {
        StartCoroutine(KillPlayerCo());
    }

    public void Restart()
    {
        StartCoroutine(RestartCo());
    }

    private IEnumerator RestartCo()
    {
        Player.Kill();
        Camera.IsFollowing = false;
        yield return new WaitForSeconds(2f);

        Camera.IsFollowing = true;

        _currentCheckpointIndex = 0;

        _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);

        _started = DateTime.UtcNow;
        GameManager.Instance.ResetPoints(0);
    }

    private IEnumerator KillPlayerCo()
    {
        Player.Kill();
        Camera.IsFollowing = false;
        yield return new WaitForSeconds(2f);

        Camera.IsFollowing = true;

        if (_currentCheckpointIndex != -1)
            _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);

        _started = DateTime.UtcNow;
        GameManager.Instance.ResetPoints(_savedPoints);
    }

    // Use this for initialization
    void Start()
    {
        _checkpoints = FindObjectsOfType<Checkpoint>().OrderBy(t => t.transform.position.x).ToList();
        _currentCheckpointIndex = _checkpoints.Count > 0 ? 0 : -1;

        Player = FindObjectOfType<Player>();
        Camera = FindObjectOfType<CameraController2D>();

        _started = DateTime.UtcNow;

        #if UNITY_EDITOR
        if (DebugSpawn != null) DebugSpawn.SpawnPlayer(Player);
        else if (_currentCheckpointIndex != -1)
            _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
#else
        if (_currentCheckpointIndex != -1)
            _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
#endif

        var listeners = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerRespawnListener>();
        foreach(var listener in listeners)
        {
            for(var i = _checkpoints.Count - 1; i >= 0; i--)
            {
                var distance = ((MonoBehaviour)listener).transform.position.x - _checkpoints[i].transform.position.x;
                if (distance < 0) continue;

                _checkpoints[i].AssignObjectToCheckpoint(listener);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var isAtLastCheckpoint = _currentCheckpointIndex + 1 >= _checkpoints.Count;
        if (isAtLastCheckpoint) return;

        var distanceToNextPoint = _checkpoints[_currentCheckpointIndex + 1].transform.position.x - Player.transform.position.x;

        if (distanceToNextPoint >= 0) return;

        _checkpoints[_currentCheckpointIndex].PlayerLeftCheckpoint();
        _currentCheckpointIndex++;
        _checkpoints[_currentCheckpointIndex].PlayerHitCheckpoint();

        GameManager.Instance.AddPoints(CurrentTimeBonus);
        _savedPoints = GameManager.Instance.Points;
        _started = DateTime.UtcNow;

    }
}
