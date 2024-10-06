using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(PlayerContoller), typeof(PlayerAnimation))]
public class Player : MonoBehaviour
{
    private PlayerInput input;
    private PlayerContoller contoller;
    private PlayerAnimation anim;

    public float movePower = 5f;
    public float sensitivity = 5f;

    public float sprintRatio = 1.2f;

    /// <summary>
    /// �ʱ�ȭ Ȯ�� �Լ�
    /// </summary>
    private bool completedInitialize = false;

    /// <summary>
    /// ���콺 �� ���� (true : ȭ�� ��, false : �� ����)
    /// </summary>
    private bool mouseLock = false;

    /// <summary>
    /// ���콺 �� ���� �� ���� ������Ƽ
    /// </summary>
    public bool MouseLock
    {
        get => mouseLock;
        set
        {
            mouseLock = value;

            if (mouseLock)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }

    private void Awake()
    {
        Init();
    }

    private void FixedUpdate()
    {
        OnPlayerMove();
    }

    private void LateUpdate()
    {
        OnPlayerLook();        
    }

    /// <summary>
    /// Player �ʱ�ȭ �Լ�
    /// </summary>
    private void Init()
    {
        input = GetComponent<PlayerInput>();
        contoller = GetComponent<PlayerContoller>();
        anim = GetComponent<PlayerAnimation>();

        // �ʱ�ȭ Ȯ��
        if(input == null || contoller == null)
        {
            completedInitialize = false;
        }
        else
        {
            MouseLock = true;
            completedInitialize = true;
        }
    }

    // Player input =====================================

    /// <summary>
    /// �÷��̾� ������ �Լ� (Update)
    /// </summary>
    private void OnPlayerMove()
    {
        if(!completedInitialize) 
            return;

        if(anim.SetSprintParam(input.GetSprintValue()))
        {
            contoller.OnMove(input.GetMoveVector() * movePower * sprintRatio);
        }
        else
        {
            contoller.OnMove(input.GetMoveVector() * movePower);
        }

        anim.SetMoveParam(input.GetMoveVector().x, input.GetMoveVector().y);
     }

    /// <summary>
    /// ī�޶� ȸ�� �Լ� (Late)
    /// </summary>
    private void OnPlayerLook()
    {
        if (!completedInitialize)
            return;

        contoller.OnLook(input.GetLookVector() * sensitivity);
    }
}