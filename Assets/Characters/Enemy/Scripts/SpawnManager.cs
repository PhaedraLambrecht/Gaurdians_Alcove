using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region SINGLETON INSTANCE
    private static SpawnManager _instance;
    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null && !ApplicationQuitting)
            {
                _instance = FindObjectOfType<SpawnManager>();
                if (_instance == null)
                {
                    GameObject newInstance = new GameObject("Singleton_SpawnManager");
                    _instance = newInstance.AddComponent<SpawnManager>();
                }
            }
            return _instance;
        }
    }

    //Checks if the singleton is alive, useful to reference it when the game is about to close down to avoid memory leaks
    public static bool Exists
    {
        get
        {
            return _instance != null;
        }
    }

    public static bool ApplicationQuitting = false;
    protected virtual void OnApplicationQuit()
    {
        ApplicationQuitting = true;
    }
    #endregion

    private void Awake()
    {
        //we want this object to persist when a scene changes
        DontDestroyOnLoad(gameObject);
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }


    private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
    public void RegisterSpawnPoint(SpawnPoint spawnPoint)
    {
        if (!_spawnPoints.Contains(spawnPoint))
            _spawnPoints.Add(spawnPoint);

    }
    public void UnRegisterSpawnPoint(SpawnPoint spawnPoint)
    {
        _spawnPoints.Remove(spawnPoint);
    }
    // Update is called once per frame
    void Update()
    {
        //remove any objects that are null
        _spawnPoints.RemoveAll(s => s == null);

        /*
        //if you do not know what predicates are: a while loop that 
        //will remove the first null it finds as long as it finds any
        while (_spawnPoints.Remove(null)) { }
        */
    }
    public void SpawnWave()
    {
        foreach (SpawnPoint point in _spawnPoints)
        {
            point.Spawn();
        }
    }
}

