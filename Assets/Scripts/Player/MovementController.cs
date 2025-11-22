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
    bool isRunPressed;
    float rotationFactorPerFrame = 1.0f;
    float runMultipler = 3f;

    private void Awake()
    {
        action = new InputSystem_Actions();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        action.Player.Move.started += onMovementInput;
        action.Player.Move.canceled += onMovementInput;
        action.Player.Move.performed += onMovementInput;
        action.Player.Sprint.started += onRun;    
        action.Player.Sprint.canceled += onRun;    

    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }
    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        CurrentMovement.z = currentMovementInput.x;
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
        if (characterController.isGrounded)
        {
            float groundedGravity = -0.05f;
            CurrentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            CurrentMovement.y = gravity;
            currentRunMovement.y = gravity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        handlerGravity();
        handleRotatio();
        HandleAnimation();

        if(isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else {
            characterController.Move(CurrentMovement * Time.deltaTime);
        }
            
                
        
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
