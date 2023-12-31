using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : BasicCharacter
{
    [SerializeField] private InputActionAsset _inputAsset;


    [SerializeField] private InputActionReference _movementAction;
    private InputAction _jumpAction;
    private InputAction _shootAction;
    private InputAction _shieldAction;


    [SerializeField] private CountDownTimer _timer = null;
    [SerializeField] private float _startDelayTime = 0.0f;
    private bool _delayStarted = true;
    public bool DelayStarted
    {
        get { return _delayStarted; }
        set { _delayStarted = value; }
    }

    public float StartDelayTime
    {
        get { return _startDelayTime; }
        private set { _startDelayTime = value; }
    }


    protected override void Awake()
    {
        base.Awake();

        if (_inputAsset == null) return;

        // Searching for the bindings in code
        _jumpAction = _inputAsset.FindActionMap("Gameplay").FindAction("Jump");
        _shootAction = _inputAsset.FindActionMap("Gameplay").FindAction("Shoot");
        _shieldAction = _inputAsset.FindActionMap("Gameplay").FindAction("Shield");


        // Bind a callback to it instead of continiously monitoring input
        _jumpAction.performed += HandleJumpInput;
    }
    protected void OnDestroy()
    {
        _jumpAction.performed -= HandleJumpInput;
    }


    private void OnEnable()
    {
        if (_inputAsset == null) 
            return;

        _inputAsset.Enable();
    }
    private void OnDisable()
    {
        if (_inputAsset == null) 
            return;

        _inputAsset.Disable();
    }


    private void Update()
    {
        HandleMovementInput();
        HandleAttackInput();
        
  
        if( (_timer != null) && (_timer.IsRunning == false) )
        {
            HandleAShieldInput();
        }
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


    private void HandleJumpInput(InputAction.CallbackContext context)
    {
        _movementBehaviour.Jump();
    }

    private void HandleAttackInput()
    {
        if (_attackBehaviour == null || _shootAction == null)
            return;

        if (_shootAction.IsPressed())
            _attackBehaviour.Attack();
    }

    private void HandleAShieldInput()
    {
        if (_shieldBehaviour == null || _shieldAction == null)
            return;

        if (_shieldAction.IsPressed() && _shieldBehaviour.IsBlocking == false)
        {
            _shieldBehaviour.Activate();
            _timer.SetCurrentTime(_startDelayTime);
        }   
    }

}



