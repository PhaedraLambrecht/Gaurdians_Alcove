using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Vector3 _previousPosition;
    private Animator _animator = null;

    private void Awake()
    {
        _previousPosition = transform.root.position;
        _animator = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovementAnimator();
    }

    const string IS_MOVING_PARAMETER = "IsMoving";
    private const float EPSILON = 0.0001f;
    void HandleMovementAnimator()
    {
        if (_animator == null)
            return;

        _animator.SetBool( IS_MOVING_PARAMETER,
                          (transform.root.position - _previousPosition).sqrMagnitude > EPSILON );

        _previousPosition = transform.root.position;
    }
}
