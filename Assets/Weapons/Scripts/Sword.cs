using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject _bulletTemplate = null;
    [SerializeField] private float _fireRate = 25.0f;
    [SerializeField] private List<Transform> _fireSockets = new List<Transform>();
    private bool _triggerPulled = false;
    private float _fireTimer = 0.0f;

    [SerializeField] private UnityEvent _onFireEvent;


    private void Update()
    {
        //handle the countdown of the fire timer
        if (_fireTimer > 0.0f)
            _fireTimer -= Time.deltaTime;

        if (_fireTimer <= 0.0f && _triggerPulled)
            FireProjectile();

        //the trigger will release by itself, 
        //if we still are firing, we will receive new fire input
        _triggerPulled = false;
    }

    private void FireProjectile()
    {
        //no bullet to fire
        if (_bulletTemplate == null)
            return;

        for (int i = 0; i < _fireSockets.Count; i++)
        {
            Instantiate(_bulletTemplate,
                _fireSockets[i].position, _fireSockets[i].rotation);
        }

        //set the time so we respect the firerate
        _fireTimer += 1.0f / _fireRate;

        _onFireEvent?.Invoke();
    }

    public void Fire()
    {
        _triggerPulled = true;
    }
}
