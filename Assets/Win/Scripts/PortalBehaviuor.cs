using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviuor : MonoBehaviour
{
    [SerializeField] private Portal _portalTemplate = null;
    [SerializeField] private GameMode _gameMode = null;

    private int _count;
    private Portal _portal = null;

    void Update()
    {
        if (_gameMode.MaxWavesReached && _count != 1)
        {
            Instantiate();
        }
    }


    void Instantiate()
    {
        _portal = Instantiate(_portalTemplate, this.transform, false);
        _portal.Activeportal = true; 
        ++_count;
    }
}
