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
            FireShield();

        //the trigger will release by itself, 
        //if we still are firing, we will receive new fire input
        _triggerPulled = false;
        _isActive = false;
    }

    private void FireShield()
    {
        _onFireEvent?.Invoke();
    }

    public void Fire()
    {
        _triggerPulled = true;
        _isActive = true;
    }
}
