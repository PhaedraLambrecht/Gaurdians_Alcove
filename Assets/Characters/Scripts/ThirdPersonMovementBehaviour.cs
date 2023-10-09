using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovementBehaviour : MovementBehaviour
{
    [SerializeField] private Transform _camera;

	protected override void HandleMovement()
	{
		if (DesiredMovementDirection.sqrMagnitude <= 0.01f)
		{
			Vector3 returnValue = Vector3.zero;

			returnValue.y = _rigidBody.velocity.y;
			_rigidBody.velocity = returnValue;
			return;
		}

		float targetAngle = Mathf.Atan2(DesiredMovementDirection.x, DesiredMovementDirection.z)
			* Mathf.Rad2Deg + _camera.transform.eulerAngles.y;

		Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

		Vector3 movement = moveDirection.normalized * _movementSpeed;
		movement.y = _rigidBody.velocity.y;
		_rigidBody.velocity = movement;
	}

	protected override void HandleRotation()
	{
		DesiredRotationDirection = DesiredRotationDirection.x * transform.right;

		base.HandleRotation();
	}
}
