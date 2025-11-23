using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine.InputSystem;
public class MovementController : MonoBehaviour
{   
    InputSystem_Actions action;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    Vector2 currentMovementInput;
    Vector3 CurrentMovement;
    Vector3 currentRunMovement;
    
    bool IsMovementPressed;
    float movementSpeed = 2f;
    bool isRunPressed;
    float rotationFactorPerFrame = 1.0f;
    float runMultipler = 3f;

    float gravity = -5f;
    float groundedGravity = -0.05f;

    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 2f;
    float maxJumpTime = 0.75f;
    bool isJumping = false;
    int isJumpingHash;
    bool isJumpAnim = false;

    private void Awake()
    {
        action = new InputSystem_Actions();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

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


    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex,  2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void handleJump()
    {
  

        if (characterController.isGrounded)
        {
            if (isJumpPressed) // solo se activa cuando se presiona el botón
            {
                isJumpPressed = false; // lo consumimos acá mismo
                isJumping = true;
                isJumpAnim = true;

                animator.SetBool(isJumpingHash, true);

                CurrentMovement.y = initialJumpVelocity;
                currentRunMovement.y = initialJumpVelocity;
            }
            else
            {
                // volver al estado normal
                if (isJumping)
                {
                    isJumping = false;
                    isJumpAnim = false;
                    animator.SetBool(isJumpingHash, false);
                }

                CurrentMovement.y = groundedGravity;
                currentRunMovement.y = groundedGravity;
            }
        }
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
        currentRunMovement.z = currentMovementInput.x * runMultipler;
        //CurrentMovement.z = currentMovementInput.y;
        IsMovementPressed = currentMovementInput.x != 0; /*|| currentMovementInput.y != 0;*/
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

    void HandleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (IsMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else if (!IsMovementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }

        if ((IsMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!IsMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    void handlerGravity()
    {
        //bool isFalling = CurrentMovement.y <= 0.0f;
        //float fallMultiplier = 2.0f;



        if (characterController.isGrounded && !isJumping)
        {
            if (isJumpAnim)
            {
                animator.SetBool(isJumpingHash, false);
                isJumpAnim = false;
            }
            CurrentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else
        {

            CurrentMovement.y += gravity * Time.deltaTime;
            currentRunMovement.y += gravity * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleRotatio();
        HandleAnimation();
            
        if(isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else {
            characterController.Move(CurrentMovement * Time.deltaTime);
        }


        handlerGravity();
        handleJump();

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
