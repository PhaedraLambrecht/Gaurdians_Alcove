using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject _shoulderObject = null;
    [SerializeField] protected float _movementSpeed = 1.0f;
    [SerializeField] protected float _rotationSpeed = 90.0f;
    [SerializeField] protected float _jumpStrength = 10.0f;

    protected Rigidbody _rigidBody;

    protected Vector3 _desiredMovementDirection = Vector3.zero;
    protected Vector3 _desiredRotationDirection = Vector3.zero;
    protected Vector3 _desiredLookAtDirection = Vector3.zero;

    protected GameObject _target;

    protected bool _grounded = false;
    protected const float GROUND_CHECK_DISTANCE = 0.2f;
    protected const string GROUND_LAYER = "Ground";

    public Vector3 DesiredMovementDirection
    {
        get { return _desiredMovementDirection; }
        set { _desiredMovementDirection = value; }
    }
    public Vector3 DesiredRotationDirection
    {
        get { return _desiredRotationDirection; }
        set { _desiredRotationDirection = value; }
    }
    public Vector3 DesiredLookAtDirection
    {
        get { return _desiredLookAtDirection; }
        set { _desiredLookAtDirection = value; }
    }
    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }
    }


    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        HandleRotation();
        //HandleLookAt();
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();

        //check if there is ground beneath our feet
        _grounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, GROUND_CHECK_DISTANCE, LayerMask.GetMask(GROUND_LAYER));
    }

    protected virtual void HandleMovement()
    {
        if (_rigidBody == null)
            return;

        Vector3 movement = _desiredMovementDirection.normalized;
        movement *= _movementSpeed;

        //maintain vertical velocity as it was otherwise gravity would be stripped out
        movement.y = _rigidBody.velocity.y;
        _rigidBody.velocity = movement;
    }

    protected virtual void HandleRotation()
    {
        if (_desiredRotationDirection.sqrMagnitude <= 0.01f) return;

        _desiredRotationDirection.Normalize();

        float targetAngle = Vector3.Angle(_desiredRotationDirection, transform.forward);

        Vector3 dir = Vector3.Cross(_desiredRotationDirection, transform.forward);
        if (dir.y > 0)
        {
            targetAngle *= -1;
        }

        targetAngle = Mathf.Clamp(targetAngle, -_rotationSpeed * Time.deltaTime, _rotationSpeed * Time.deltaTime);

        transform.Rotate(0.0f, targetAngle, 0.0f);
    }

    protected virtual void HandleLookAt()
    {

        if (_shoulderObject == null) return;
      
        _shoulderObject.transform.LookAt(_desiredLookAtDirection);
    }

    public void Jump()
    {
        if (_grounded)
            _rigidBody.AddForce(Vector3.up * _jumpStrength, ForceMode.Impulse);
    }
}




