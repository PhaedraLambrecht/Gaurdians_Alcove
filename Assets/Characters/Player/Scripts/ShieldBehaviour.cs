using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShieldBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _shieldTemplate = null;
    [SerializeField] private Transform _socket = null;
    [SerializeField] private float _activeTime = 0.0f;

    private GameObject _shield = null;
    private float _timer = 0.0f;

    [SerializeField] private UnityEvent _onDeactivateEvent;    
    [SerializeField] private UnityEvent _onActivateEvent;

    private bool _isBlocking;
    public bool IsBlocking
    {
        get { return _isBlocking; }
        private set { _isBlocking = value; }
    }


    private void Update()
    {
        if (_shield == null)
            return;

        if(_isBlocking)
        {
            if(_timer <= 0)
            {
                Deactivate();
            }
            else
            {
                _timer -= Time.deltaTime;
            }

        }  
    }



    const string SHIELD_TAG = "Shield";
    public void Activate()
    {
        if (_shieldTemplate != null && _socket != null)
        {
            // Create the shield and play the sound
            _shield = Instantiate(_shieldTemplate, _socket, false);
            _onActivateEvent?.Invoke();

            // anything with the shield tag can be a shield (yes even a human)
            if (_shield.tag == SHIELD_TAG) 
                _isBlocking = true;
        }

        // Set the timers new value
        _timer = _activeTime;
    }


    public void Deactivate()
    {
        _isBlocking = false;

        if (_shield != null)
        {
            Destroy(_shield);
            _onDeactivateEvent?.Invoke();
        }
    }
}
