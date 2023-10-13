using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Will always target the player
public class EnemyArcherCharacter : BasicEnemy
{
    protected GameObject _playerTarget = null;

    protected void Start()
    {
        //expensive method, use with caution
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        if (player) _playerTarget = player.gameObject;
    }

    protected override void Update()
    {
        base.Update();
    }


    protected override void HandleMovement()
    {
        base.HandleMovement();

        _movementBehaviour.DesiredLookAtDirection = _playerTarget.transform.position;
        _movementBehaviour.Target = _playerTarget;
    }

    protected override void HandleAttacking()
    {
        base.HandleAttacking();

        if (_playerTarget == null)
            return;

        //if we are in range of the player, fire our weapon, 
        //use sqr magnitude when comparing ranges as it is more efficient
        if ((transform.position - _playerTarget.transform.position).sqrMagnitude > _attackRange * _attackRange)
        {
            _hasAttacked = true;
            _attackBehaviour.Attack();
        }
    }
}
