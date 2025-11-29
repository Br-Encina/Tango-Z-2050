using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class PlayerStateMachine : MonoBehaviour
{
    InputSystem_Actions action;
    CharacterController characterController;
    public CharacterController CharacterController { get { return characterController; } }
    Animator animator;

    

    #region Movement Variables

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


    public float CurrentMovementY { get { return currentMovement.y; } set { currentMovement.y = value; } }
    public float CurrentMovementZ { get { return currentMovement.z; } set { currentMovement.z = value; } }
    public float ApliedMovementY { get { return appliedMovement.y; } set { appliedMovement.y = value; } }
    public Animator Animator { get { return animator; } }
    public float Gravity { get { return gravity; } }
    
    public bool IsRunPressed { get { return isRunPressed; } }
    public bool IsMovementPressed { get { return isMovementPressed; } }
    public int IsWalkingHash { get { return isWalkingHash; } }
    public int IsRunningHash { get { return isRunningHash; } }

    public float ApliedMovementZ { get { return appliedMovement.z; } set { appliedMovement.z = value; } }
    public Vector3 CurrentMovementInput { get { return currentMovementInput; } }
    public float RunMultipler { get { return runMultipler; } }

    #endregion

    #region Jump Variables
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
    public float InitialJumpVelocity { get { return initialJumpVelocity; } }
    #endregion


    #region Push Box Interaction
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

    #endregion

    #region Aim Variables
    bool gunIsPicked = false;
    public bool GunIsPickeded { get { return gunIsPicked; } set { gunIsPicked = value; } }
    int isAimingHash;
    public int IsAimingHash { get { return isAimingHash; } }
    public bool IsAimPressed { get { return isAimPressed; } set { isAimPressed = value; } }

    bool isAimPressed = false;

    bool isShootpressed = false;
    public bool IsShootPressed { get { return isShootpressed; } set { isShootpressed = value; } }

    int shootTriggerHash;
    public int ShootTriggerHash { get { return shootTriggerHash; } }


    int maxAmmo = 10;
    public int MaxAmmo { get { return maxAmmo; } }
    int currentAmmo;
    public int CurrentAmmo { get { return currentAmmo; } set { currentAmmo = value; } }
    bool hasAmmo => currentAmmo > 0;
    public bool HasAmmo { get { return hasAmmo; } }

    #endregion

    #region Climb Variables

    bool IsTouchingLadder;
    Ladder CurrentLadder;

    float LadderSpeed = 2f;
    bool IsClimbingPressed;

    #endregion

    #region Death Variables

     bool isDead = false;
    public bool IsDead { get { return isDead; } set { isDead = value; } }

    int isDeadHash;
    public int IsDeadHash { get { return isDeadHash; } set { isDeadHash = value; } }
    #endregion


    PlayerBaseState currentState;
    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }
   
    PlayerStateFactory states;

    public string newcurrentState;





    private void Awake()
    {
        action = new InputSystem_Actions();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        //CurrentLadder = Find
        

        states = new PlayerStateFactory(this);
        currentState = states.Grounded();
        currentState.EnterState();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isPushingHash = Animator.StringToHash("isPushing");
        isAimingHash = Animator.StringToHash("isAiming");
        isDeadHash = Animator.StringToHash("isDead");
        //shootTriggerHash = Animator.StringToHash("shootTrigger");


        action.Player.Move.started += onMovementInput;
        action.Player.Move.canceled += onMovementInput;
        action.Player.Move.performed += onMovementInput;
        action.Player.Sprint.started += onRun;
        action.Player.Sprint.canceled += onRun;
        action.Player.Jump.started += onJump;
        action.Player.Jump.canceled += onJump;
        action.Player.Push.started += onPush;
        action.Player.Push.canceled += onPush;
        action.Player.Aim.started += onAim;
        action.Player.Aim.canceled += onAim;
        action.Player.Attack.started += onShoot;
        action.Player.Attack.canceled += onShoot;


        setupJumpVariables();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = maxAmmo;
        characterController.Move(appliedMovement * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        


        handleRotatio();

        characterController.Move(appliedMovement * Time.deltaTime);
        currentState.UpdateStates();
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
    //public void PíckGun()
    //{
    //    gunIsPicked = true;
    //}
    void onShoot(InputAction.CallbackContext context)
    {
        isShootpressed = context.ReadValueAsButton();
    }

    void onAim(InputAction.CallbackContext context)
    {
        isAimPressed = context.ReadValueAsButton();
    }

    void onPush(InputAction.CallbackContext context)
    {
        isPushPressed = context.ReadValueAsButton();
    }
    void DetectPushableBox()
    {
        Vector3 origin = transform.position + Vector3.up * 1f;
        Vector3 direction = transform.forward;
        float distance = 1f;

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

    public void Deathpanel()
    {
        DeathUIManager ui = GameObject.FindAnyObjectByType<DeathUIManager>();
        ui.ShowDeathScreen();
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
