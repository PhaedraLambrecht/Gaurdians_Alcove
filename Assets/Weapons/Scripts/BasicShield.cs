using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicShield : MonoBehaviour
{
    private bool _triggerPulled = false;
    public bool _isActive = false;

    [SerializeField] private UnityEvent _onFireEvent;


    private void Update()
    {
        if (_triggerPulled)
            FireEvent();

        //the trigger will release by itself, 
        _triggerPulled = false;
        _isActive = false;
    }

    private void FireEvent()
    {
        _onFireEvent?.Invoke();
    }

    public void FireShield()
    {
        _triggerPulled = true;
        _isActive = true;
    }
}
