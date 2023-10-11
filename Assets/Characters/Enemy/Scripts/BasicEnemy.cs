using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : BasicCharacter
{
    protected GameObject _playerTarget = null;
    [SerializeField] protected float _attackRange = 10.0f;

    protected void Start()
    {
        //expensive method, use with caution
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player) _playerTarget = player.gameObject;
    }

}
