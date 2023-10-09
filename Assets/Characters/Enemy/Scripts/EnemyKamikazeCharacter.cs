using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikazeCharacter : BasicCharacter
{
    private GameObject _playerTarget = null;
    [SerializeField] private float _attackRange = 2.0f;
    [SerializeField] GameObject _AttackVFXTemplate = null;

    private bool _hasAttacked = false;
    

    private void Start()
    {
        //expensive method, use with caution
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player) _playerTarget = player.gameObject;
    }

    private void Update()
    {
        HandleMovement();
        HandleAttacking();
    }

    void HandleMovement()
    {
        if (_movementBehaviour == null)
            return;

        _movementBehaviour.Target = _playerTarget;
    }

    void HandleAttacking()
    {
        if (_hasAttacked) 
            return;

        if (_attackBehaviour == null) 
            return;

        if (_playerTarget == null) 
            return;

        //if we are in range of the player, fire our weapon, 
        //use sqr magnitude when comparing ranges as it is more efficient
        if ((transform.position - _playerTarget.transform.position).sqrMagnitude
            < _attackRange * _attackRange)
        {
            _hasAttacked = true;
            _attackBehaviour.Attack();

            if(_AttackVFXTemplate)
                Instantiate(_AttackVFXTemplate, transform.position, transform.rotation);

            //this is a kamikaze enemy, 
            //when it fires, it should destroy itself
            //we do this with a delay so other logic (like player feedback and the                   attack, will have the time to execute)
            Invoke("Kill", 0.2f);
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}

