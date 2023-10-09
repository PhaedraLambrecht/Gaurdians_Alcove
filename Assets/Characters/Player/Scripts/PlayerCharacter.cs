using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : BasicCharacter
{
    [SerializeField]
    private InputActionAsset _inputAsset;

    [SerializeField]
    private InputActionReference _movementAction;

    private InputAction _jumpAction;
    private InputAction _shootAction;
    private InputAction _shieldAction;

    protected override void Awake()
    {
        base.Awake();

        if (_inputAsset == null) return;

        //example of searching for the bindings in code, alternatively, they can be hooked in the editor using a InputAcctionReference as shown by _movementAction
        _jumpAction = _inputAsset.FindActionMap("Gameplay").FindAction("Jump");
        _shootAction = _inputAsset.FindActionMap("Gameplay").FindAction("Shoot");
        _shieldAction = _inputAsset.FindActionMap("Gameplay").FindAction("Shield");

        //we bind a callback to it instead of continiously monitoring input
        _jumpAction.performed += HandleJumpInput;
    }
    protected void OnDestroy()
    {
        _jumpAction.performed -= HandleJumpInput;
    }


    private void OnEnable()
    {
        if (_inputAsset == null) return;

        _inputAsset.Enable();
    }
    private void OnDisable()
    {
        if (_inputAsset == null) return;

        _inputAsset.Disable();
    }
    private void Update()
    {
        HandleMovementInput();
        HandleAttackInput();
        HandleAShieldInput();
       // HandleAimingInput();
    }
    void HandleMovementInput()
    {
        if (_movementBehaviour == null || _movementAction == null)
            return;

        //movement
        Vector2 movementInput = _movementAction.action.ReadValue<Vector2>().normalized;

        Vector3 movement = movementInput.x * Vector3.right + movementInput.y * Vector3.forward;

        _movementBehaviour.DesiredMovementDirection = movement;

        // if we are moving forwards/backwards, we don't want to rotate our character
        _movementBehaviour.DesiredRotationDirection = movement;
    }

    private void HandleAimingInput()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = Mathf.Abs(transform.position.z - Camera.main.transform.position.z); // the mouse position represents a line, the z coordinate represents the distance from the camera we pick a point on this line
       
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldMousePosition.z = 0; //or game takes place in xy plane, so remove z position
      
        _movementBehaviour.DesiredLookAtDirection = worldMousePosition;
    }

    private void HandleJumpInput(InputAction.CallbackContext context)
    {
        _movementBehaviour.Jump();
    }

    private void HandleAttackInput()
    {
        if (_attackBehaviour == null
            || _shootAction == null)
            return;

        if (_shootAction.IsPressed())
            _attackBehaviour.Attack();
    }
    private void HandleAShieldInput()
    {
        if (_shieldBehaviour == null
            || _shieldAction == null)
            return;

        if (_shieldAction.IsPressed())
            _shieldBehaviour.Attack();
        else
            _shieldBehaviour.Released();
    }

}



