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
    Vector2 loockVec;

    /// <summary>
    /// 스프린트 확인 변수
    /// </summary>
    bool isSprint = false;

    /// <summary>
    /// 점프키 누름 여부 변수
    /// </summary>
    bool isJump = false;

    /// <summary>
    /// 사격키 누름 여부 변수
    /// </summary>
    bool isShot = false;

    /// <summary>
    /// 조준 누름 여부 변수
    /// </summary>
    bool isZoomin = false;

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
        loockVec = context.ReadValue<Vector2>();
    }

    private void OnSprintInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprint = true;
        }
        else
        {
            isSprint = false;
        }
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }
    }

    private void OnShotInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isShot = true;
        }
        else
        {
            isShot = false;
        }
    }

    private void OnZoomInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isZoomin = true;
        }
        else
        {
            isZoomin = false;
        }
    }

    /// <summary>
    /// 움직임 입력 값을 반환하는 함수
    /// </summary>
    /// <returns>움직임 입력 값</returns>
    public Vector2 GetMoveVector()
    {
        return moveVec;
    }

    /// <summary>
    /// 마우스 입력 값을 변환하는 함수
    /// </summary>
    /// <returns>마우스 입력 값</returns>
    public Vector2 GetLookVector()
    {
        return loockVec;
    }

    /// <summary>
    /// 스프린트 입력 반환 변수 (LShift)
    /// </summary>
    /// <returns>LShift 누름 여부(눌렀으면 true 아니면 false)</returns>
    public bool GetSprintValue()
    {
        return isSprint;
    }

    /// <summary>
    /// 점프 입력값 반환 함수 (Space)
    /// </summary>
    /// <returns>눌렀으면 true 아니면 false</returns>
    public bool GetJumpPressValue()
    {
        return isJump;
    }

    /// <summary>
    /// 사격 입력값 반환 함수(LM)
    /// </summary>
    /// <returns>눌렀으면 true 아니면 false</returns>
    public bool GetShotPressValue()
    {
        return isShot;
    }

    /// <summary>
    /// 조준 입력값 반환 함수
    /// </summary>
    /// <returns>눌렀으면 true 아니면 false</returns>
    public bool GetZoomPressValue()
    {
        return isZoomin;
    }
}
