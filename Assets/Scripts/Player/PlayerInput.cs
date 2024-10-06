using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    PlayerInputActions inputActions;

    /// <summary>
    /// ������ ���Ͱ�
    /// </summary>
    Vector2 moveVec;

    /// <summary>
    /// ���콺 ����
    /// </summary>
    Vector2 loockVec;

    /// <summary>
    /// ������Ʈ Ȯ�� ����
    /// </summary>
    bool isSprint = false;

    // unity ====================================

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        Initialize_Move();
    }

    private void OnDisable()
    {
        Initialize_Move_Remove();
    }

    // initialize ====================================

    private void Initialize_Move()
    {
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
        inputActions.Player.Look.performed += OnLookInput;
        inputActions.Player.Look.canceled += OnLookInput;
        inputActions.Player.Sprint.performed += OnSprintInput;
        inputActions.Player.Sprint.canceled += OnSprintInput;
    }


    private void Initialize_Move_Remove()
    {
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Look.performed -= OnLookInput;
        inputActions.Player.Look.canceled -= OnLookInput;
        inputActions.Player.Sprint.performed -= OnSprintInput;
        inputActions.Player.Sprint.canceled -= OnSprintInput;
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
        if(context.performed)
        {
            isSprint = true;
        }
        else
        {
            isSprint = false;
        }
    }

    /// <summary>
    /// ������ �Է� ���� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>������ �Է� ��</returns>
    public Vector2 GetMoveVector()
    {
        return moveVec;
    }

    /// <summary>
    /// ���콺 �Է� ���� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>���콺 �Է� ��</returns>
    public Vector2 GetLookVector()
    {
        return loockVec;
    }

    /// <summary>
    /// ������Ʈ �Է� ��ȯ ���� (LShift)
    /// </summary>
    /// <returns>LShift ���� ����(�������� true �ƴϸ� false)</returns>
    public bool GetSprintValue()
    {
        return isSprint;
    }
}
