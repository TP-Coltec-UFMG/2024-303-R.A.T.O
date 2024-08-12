using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance;

    public Vector2 MoveInput {get; private set;}
    public bool JumpInput {get; private set;}
    public bool InteractInput {get; private set;}
    public bool AttackInput {get; private set;}
    public float RunInput {get; private set;}

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction interactAction;
    private InputAction attackAction;
    private InputAction runAction;

    private PlayerInput playerInput;

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }

        playerInput = GetComponent<PlayerInput>();

        SetupInputActions();
    }

    private void SetupInputActions(){
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        interactAction = playerInput.actions["Interact"];
        attackAction = playerInput.actions["Attack"];
        runAction = playerInput.actions["Run"];
    }

    private void UpdateInputs(){
        MoveInput = moveAction.ReadValue<Vector2>();
        JumpInput = jumpAction.WasPressedThisFrame();
        InteractInput = interactAction.WasPressedThisFrame();
        AttackInput = attackAction.WasPressedThisFrame();
        RunInput = runAction.ReadValue<float>();
    }

    private void Update(){
        UpdateInputs();
    }
}
