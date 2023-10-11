using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyArcherCharacter : BasicCharacter
{
    private GameObject _playerTarget = null;
    [SerializeField] private float _attackRange = 10.0f;


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

        _movementBehaviour.DesiredLookAtDirection = _playerTarget.transform.position;

        if( (transform.position - _playerTarget.transform.position).sqrMagnitude
            > _attackRange)
        {
            _movementBehaviour.Target = _playerTarget;
        }
        else
        {
            _movementBehaviour.Target = null;
        }
     

    }



    void HandleAttacking()
    {
        if (_attackBehaviour == null)
            return;

        if (_playerTarget == null)
            return;

        //if we are in range of the player, fire our weapon, 
        //use sqr magnitude when comparing ranges as it is more efficient
        _attackBehaviour.Attack();
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
