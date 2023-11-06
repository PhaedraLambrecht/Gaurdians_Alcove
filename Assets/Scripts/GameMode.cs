using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameMode : MonoBehaviour
{
    [SerializeField] private float _firstWaveStart = 5.0f;
    [SerializeField] private float _waveStartFrequency = 15.0f;
    [SerializeField] private float _waveEndFrequency = 7.0f;
    [SerializeField] private float _waveFrequencyIncrement = 0.5f;
    [SerializeField] private int _maxWaves = 1;

    private int _currentWave = 0;
    public bool  MaxWavesReached
    {
        get { return _currentWave == _maxWaves; }
    }


    private float _currentFrequency = 0.0f;
    void Awake()
    {
            _currentFrequency = _waveStartFrequency;

            Invoke(STARTNEWWAVE_METHOD, _firstWaveStart);
    }

    const string STARTNEWWAVE_METHOD = "StartNewWave";
    void StartNewWave()
    {
        if (_currentWave == _maxWaves) return;

            SpawnManager.Instance.SpawnWave();

        _currentFrequency = Mathf.Clamp(_currentFrequency - _waveFrequencyIncrement,
            _waveEndFrequency, _waveStartFrequency);

        Invoke(STARTNEWWAVE_METHOD, _currentFrequency);

        _currentWave += 1;
    }
}

