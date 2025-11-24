using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    InputSystem_Actions action;
    CharacterController characterController;
    public CharacterController CharacterController { get { return characterController; } }
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 appliedMovement;

    float movementSpeed = 2f;
    bool isMovementPressed;
    bool isRunPressed;
    float rotationFactorPerFrame = 1.0f;
    float runMultipler = 3f;
    float gravity = -9.8f;
    

    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 2f;
    float maxJumpTime = 0.75f;
    bool isJumping = false;
    int isJumpingHash;
    bool requireNewJumpPress = false;
    public bool RequireNewJumpPress { get { return requireNewJumpPress; } set { requireNewJumpPress = value; } }
    public bool IsJumping { get { return isJumping; } set { isJumping = value; } }
    public int IsJumpingHash { get { return isJumpingHash; } }
    public bool IsJumpPressed { get { return isJumpPressed; } set { isJumpPressed = value; } }
    public float CurrentMovementY { get { return currentMovement.y; } set { currentMovement.y = value; } }
    public float ApliedMovementY { get { return appliedMovement.y; } set { appliedMovement.y = value; } }
    public Animator Animator { get { return animator; } }
    public float InitialJumpVelocity { get { return initialJumpVelocity; } }
    public float Gravity { get { return gravity; } }
    
    public bool IsRunPressed { get { return isRunPressed; } }
    public bool IsMovementPressed { get { return isMovementPressed; } }
    public int IsWalkingHash { get { return isWalkingHash; } }
    public int IsRunningHash { get { return isRunningHash; } }

    public float ApliedMovementZ { get { return appliedMovement.z; } set { appliedMovement.z = value; } }
    public Vector3 CurrentMovementInput { get { return currentMovementInput; } }
    public float RunMultipler { get { return runMultipler; } }

    //Push Box Interaction
    bool isPushPressed = false;
    public bool IsPushPressed { get { return isPushPressed; } }
    bool isTouchingPushBox = false;
    public bool IsTouchingPushBox { get { return isTouchingPushBox; } set { isTouchingPushBox = value; } }
    PushableBox currentPushBox;
    public PushableBox CurrentPushBox { get { return currentPushBox; } }
    float pushSpeed = 1.2f;
    public float PushSpeed { get { return pushSpeed; } }

    int isPushingHash;
    public int IsPushingHash { get { return isPushingHash; } set { isPushingHash = value; } }






    PlayerBaseState currentState;
    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }
   
    PlayerStateFactory states;

    public string newcurrentState;





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
        action.Player.Push.started += onPush;
        action.Player.Push.canceled += onPush;


        setupJumpVariables();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController.Move(appliedMovement * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        


        handleRotatio();

        characterController.Move(appliedMovement * Time.deltaTime);
        currentState.UpdateStates();

        //if (isRunPressed)
        //{
        //    characterController.Move(currentRunMovement * Time.deltaTime);
        //}
        //else
        //{
        //    characterController.Move(CurrentMovement * Time.deltaTime);
        //}
        DetectPushableBox();
        OnSwithState(currentState);


    }
    void handleRotatio()
    {
        Vector3 positionTolookAt;

        positionTolookAt.x = currentMovement.x;
        positionTolookAt.y = 0.0f;
        positionTolookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;



        if (isMovementPressed)
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

    void onPush(InputAction.CallbackContext context)
    {
        isPushPressed = context.ReadValueAsButton();
    }
    void DetectPushableBox()
    {
        Vector3 origin = transform.position + Vector3.up * 1f;
        Vector3 direction = transform.forward;
        float distance = 0.7f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            if (hit.collider.TryGetComponent(out PushableBox pushBox))
            {
                isTouchingPushBox = true;
                currentPushBox = pushBox;
                return;
            }
        }

        isTouchingPushBox = false;
        currentPushBox = null;
    }

    void onJump(InputAction.CallbackContext context)
    {
        //isJumpPressed = context.ReadValueAsButton();
        //Debug.Log("Jump Pressed: " + isJumpPressed);

        if (context.started)
        {
            isJumpPressed = true;
        }
        else if (context.canceled)
        {
            isJumpPressed = false;
            requireNewJumpPress = false;
        }
    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }
    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.z = currentMovementInput.x * movementSpeed;
        appliedMovement.z = currentMovementInput.x * runMultipler;
        
        isMovementPressed = currentMovementInput.x != 0;
    }

    private void OnEnable()
    {
        action.Player.Enable();
    }

    private void OnDisable()
    {
        action.Player.Disable();
    }

    void OnSwithState(PlayerBaseState currentState)
    {
        if(newcurrentState != currentState.GetType().Name)
        {
            newcurrentState = currentState.GetType().Name;
            Debug.Log("Current Player State: " + newcurrentState);
        }
    }


}
