using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemy : BasicCharacter
{
    [SerializeField] protected float _attackRange = 10.0f;
    protected bool _hasAttacked = false;

    protected virtual void Update()
    {
        HandleMovement();
        HandleAttacking();
    }


    protected virtual void HandleMovement()
    {
        if (_movementBehaviour == null)
            return;

        _movementBehaviour.Target = null;
    }
    protected virtual void HandleAttacking()
    {
        if (_hasAttacked)
            return;

        if (_attackBehaviour == null)
            return;

    }
}
