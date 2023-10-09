using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _shieldTemplate = null;
    [SerializeField] private GameObject _socket = null;
    private BasicShield _shield = null;

    private void Awake()
    {
        if (_shieldTemplate != null && _socket != null)
        {
            var shieldObject = Instantiate(_shieldTemplate, _socket.transform, true);
            shieldObject.transform.localPosition = Vector3.zero;
            shieldObject.transform.localRotation = Quaternion.identity;
            _shield = shieldObject.GetComponent<BasicShield>();
        }

        _shield.gameObject.SetActive(false);

    }

    public void Attack()
    {
        if (_shield != null)
        {
            _shield.Fire();
            _shield.gameObject.SetActive(true);
        }
    }


    public void Released()
    {
        if (_shield != null)
        {
            _shield.gameObject.SetActive(false);

        }
    }
}
