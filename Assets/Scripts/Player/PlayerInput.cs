using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    PlayerInputActions inputActions;

    /// <summary>
    /// 움직임 벡터값
    /// </summary>
    Vector2 moveVec;

    /// <summary>`
    /// 마우스 벡터
    /// </summary>
    Vector2 lookVec;

    /// <summary>
    /// 스프린트 확인 변수
    /// </summary>
    bool isSprint = false;

    bool isJump = false;

    /// <summary>
    /// 움직임 입력 시 실행되는 델리게이트
    /// </summary>
    public Action<Vector2, bool> OnMove;

    /// <summary>
    /// 마우스 입력 시 실행되는 델리게이트
    /// </summary>
    public Action<Vector2> OnLook;

    /// <summary>
    /// 점프 입력 시 실행되는 델리게이트
    /// </summary>
    public Action<bool> OnJump;

    /// <summary>
    /// 사격키 입력 시 실행되는 델리게이트
    /// </summary>
    public Action<bool> OnShot;

    /// <summary>
    /// 줌 시작 시 실행되는 델리게이트
    /// </summary>
    public Action<bool> OnZoomIn;

    // unity ====================================

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        Initialize_PlayerAction();
    }

    private void OnDisable()
    {
        PlayerAction_Remove();
        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        if (!inputActions.Player.enabled)
            return;

        OnMove?.Invoke(moveVec, isSprint);
        OnJump?.Invoke(isJump);
    }

    private void LateUpdate()
    {
        if (!inputActions.Player.enabled)
            return;
        
        OnLook?.Invoke(lookVec);
    }

    // initialize ====================================

    private void Initialize_PlayerAction()
    {
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
        inputActions.Player.Look.performed += OnLookInput;
        inputActions.Player.Look.canceled += OnLookInput;
        inputActions.Player.Sprint.performed += OnSprintInput;
        inputActions.Player.Sprint.canceled += OnSprintInput;
        inputActions.Player.Jump.performed += OnJumpInput;
        inputActions.Player.Jump.canceled += OnJumpInput;
        inputActions.Player.Shot.performed += OnShotInput;
        inputActions.Player.Shot.canceled += OnShotInput;
        inputActions.Player.ZoomIn.performed += OnZoomInput;
        inputActions.Player.ZoomIn.canceled += OnZoomInput;
    }

    private void PlayerAction_Remove()
    {
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Look.performed -= OnLookInput;
        inputActions.Player.Look.canceled -= OnLookInput;
        inputActions.Player.Sprint.performed -= OnSprintInput;
        inputActions.Player.Sprint.canceled -= OnSprintInput;
        inputActions.Player.Jump.performed -= OnJumpInput;
        inputActions.Player.Jump.canceled -= OnJumpInput;
        inputActions.Player.Shot.performed -= OnShotInput;
        inputActions.Player.Shot.canceled -= OnShotInput;
        inputActions.Player.ZoomIn.performed += OnZoomInput;
        inputActions.Player.ZoomIn.canceled += OnZoomInput;
    }

    // Input ====================================

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        moveVec = context.ReadValue<Vector2>();
    }

    private void OnLookInput(InputAction.CallbackContext context)
    {
        lookVec = context.ReadValue<Vector2>();
    }

    private void OnSprintInput(InputAction.CallbackContext context)
    {
        isSprint = context.performed;
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        isJump = context.performed;
    }

    private void OnShotInput(InputAction.CallbackContext context)
    {
        OnShot?.Invoke(context.performed);
    }

    private void OnZoomInput(InputAction.CallbackContext context)
    {
        OnZoomIn?.Invoke(context.performed);    
    }
}
