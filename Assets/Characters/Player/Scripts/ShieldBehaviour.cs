using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _shieldTemplate = null;
    [SerializeField] private Transform _socket = null;
    private GameObject _shield = null;

    private bool _isBlocking;
    public bool IsBlocking
    {
        get { return _isBlocking; }
        private set { _isBlocking = value; }
    }

    private void Update()
    {
        if (_shield != null)
            return;

        if(_isBlocking)
    }



    const string SHIELD_TAG = "Shield";
    public void Attack()
    {
        if (_shieldTemplate != null && _socket != null)
        {
            _shield = Instantiate(_shieldTemplate, _socket, false);


            if (_shield.tag == SHIELD_TAG) _isBlocking = true;
        }
    }


    public void Released()
    {

        _isBlocking = false;

        if (_shield != null)
        {
            Destroy(_shield);
        }
    }
}
