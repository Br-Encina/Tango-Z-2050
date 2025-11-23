using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    InputSystem_Actions action;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    Vector2 currentMovementInput;
    Vector3 CurrentMovement;
    Vector3 currentRunMovement;
    Vector3 appliedMovement;

    float movementSpeed = 2f;
    bool IsMovementPressed;
    bool isRunPressed;
    float rotationFactorPerFrame = 1.0f;
    float runMultipler = 3f;
    int zero = 0; 

    float gravity = -9.8f;
    float groundedGravity = -0.05f;

    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 2f;
    float maxJumpTime = 0.75f;
    bool isJumping = false;
    int isJumpingHash;
    bool isJumpAnim = false;


    PlayerBaseState currentState;
    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    PlayerStateFactory states;



    private void Awake()
    {
        action = new InputSystem_Actions();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        states = new PlayerStateFactory(this);
        currentState = states.Grounded();
        currentState.EnterState();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");

        action.Player.Move.started += onMovementInput;
        action.Player.Move.canceled += onMovementInput;
        action.Player.Move.performed += onMovementInput;
        action.Player.Sprint.started += onRun;
        action.Player.Sprint.canceled += onRun;
        action.Player.Jump.started += onJump;
        action.Player.Jump.canceled += onJump;


        setupJumpVariables();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        handleRotatio();

        characterController.Move(appliedMovement * Time.deltaTime);

        //if (isRunPressed)
        //{
        //    characterController.Move(currentRunMovement * Time.deltaTime);
        //}
        //else
        //{
        //    characterController.Move(CurrentMovement * Time.deltaTime);
        //}



    }
    void handleRotatio()
    {
        Vector3 positionTolookAt;

        positionTolookAt.x = CurrentMovement.x;
        positionTolookAt.y = 0.0f;
        positionTolookAt.z = CurrentMovement.z;

        Quaternion currentRotation = transform.rotation;



        if (IsMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionTolookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame);
        }
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void onJump(InputAction.CallbackContext context)
    {
        //isJumpPressed = context.ReadValueAsButton();
        //Debug.Log("Jump Pressed: " + isJumpPressed);

        if (context.started)
        {
            isJumpPressed = true;
        }
    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }
    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        CurrentMovement.z = currentMovementInput.x * movementSpeed;
        //currentRunMovement.z = currentMovementInput.x * runMultipler;
        
        IsMovementPressed = currentMovementInput.x != 0;
    }

    private void OnEnable()
    {
        action.Player.Enable();
    }

    private void OnDisable()
    {
        action.Player.Disable();
    }
}
