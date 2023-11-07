using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikazeCharacter : BasicEnemy
{
    protected GameObject _target = null;
    [SerializeField] GameObject _AttackVFXTemplate = null;

    private void Start()
    {
        //expensive method, use with caution
        BasicArtifact artifact = FindObjectOfType<BasicArtifact>();
        if (artifact) _target = artifact.gameObject;

        if(_target == null)
        {
            PlayerCharacter character = FindAnyObjectByType<PlayerCharacter>();
            if (character) _target = character.gameObject;
        }
    }

    protected override void Update()
    {
       base.Update();
    }


    protected override void HandleMovement()
    {
      base.HandleMovement();

        _movementBehaviour.Target = _target;
    }

    protected override void HandleAttacking()
    {
        base.HandleAttacking();

        if (_target == null)
            return;

        //if we are in range of the player, fire our weapon, 
        //use sqr magnitude when comparing ranges as it is more efficient
        if ((transform.position - _target.transform.position).sqrMagnitude
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
        Health health = this.GetComponent<Health>();
        health.Kill();

        Destroy(gameObject);
    }
}

