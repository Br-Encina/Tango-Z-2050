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

    Vector2 currentMovementInput;
    Vector3 CurrentMovement;
    bool IsMovementPressed;
    float rotationFactorPerFrame = 1.0f;

    private void Awake()
    {
        action = new InputSystem_Actions();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        action.Player.Move.started += onMovementInput;
        action.Player.Move.canceled += onMovementInput;
        action.Player.Move.performed += onMovementInput;

    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        CurrentMovement.z = currentMovementInput.x;
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
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        if (IsMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else if (!IsMovementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleRotatio();
        HandleAnimation();
        characterController.Move(CurrentMovement * Time.deltaTime);
        
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
