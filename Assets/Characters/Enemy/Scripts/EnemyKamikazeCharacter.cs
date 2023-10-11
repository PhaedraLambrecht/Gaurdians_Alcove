using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikazeCharacter : BasicCharacter
{
    protected GameObject _artifactTarget = null;
    [SerializeField] protected float _attackRange = 10.0f;
    [SerializeField] GameObject _AttackVFXTemplate = null;

    private bool _hasAttacked = false;

    private void Start()
    {
        //expensive method, use with caution
        BasicArtifact artifact = FindObjectOfType<BasicArtifact>();

        if (artifact) _artifactTarget = artifact.gameObject;
    }

    private void Update()
    {
        HandleMovement();
        HandleAttacking();
    }

    private void HandleMovement()
    {
        if (_movementBehaviour == null)
            return;

        _movementBehaviour.Target = _artifactTarget;
    }

    private void HandleAttacking()
    {
        if (_hasAttacked) 
            return;

        if (_attackBehaviour == null)
            return;

        if (_artifactTarget == null)
            return;

        //if we are in range of the player, fire our weapon, 
        //use sqr magnitude when comparing ranges as it is more efficient
        if ((transform.position - _artifactTarget.transform.position).sqrMagnitude
            < _attackRange * _attackRange)
        {
            _hasAttacked = true;
            _attackBehaviour.Attack();

            if(_AttackVFXTemplate)
                Instantiate(_AttackVFXTemplate, transform.position, transform.rotation);

            //this is a kamikaze enemy, 
            //when it fires, it should destroy itself
            //we do this with a delay so other logic (like player feedback and the attack, will have the time to execute)
            Invoke("Kill", 0.2f);
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}

