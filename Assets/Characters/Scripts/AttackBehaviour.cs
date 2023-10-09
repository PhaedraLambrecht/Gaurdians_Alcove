using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _gunTemplate = null;

    [SerializeField]
    private GameObject _socket = null;

    private BasicWeapon _weapon = null;

    void Awake()
    {
        //spawn guns
        if (_gunTemplate != null && _socket != null)
        {
            var gunObject = Instantiate(_gunTemplate,
                _socket.transform, true);
            gunObject.transform.localPosition = Vector3.zero;
            gunObject.transform.localRotation = Quaternion.identity;
            _weapon = gunObject.GetComponent<BasicWeapon>();
        }

    }

    public void Attack()
    {
        if (_weapon != null)
            _weapon.Fire();
    }
}



