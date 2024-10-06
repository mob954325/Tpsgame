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
    /// 초기화 확인 함수
    /// </summary>
    private bool completedInitialize = false;

    /// <summary>
    /// 마우스 락 여부 (true : 화면 락, false : 락 해제)
    /// </summary>
    private bool mouseLock = false;

    /// <summary>
    /// 마우스 락 접근 및 수정 프로퍼티
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
    /// Player 초기화 함수
    /// </summary>
    private void Init()
    {
        input = GetComponent<PlayerInput>();
        contoller = GetComponent<PlayerContoller>();
        anim = GetComponent<PlayerAnimation>();

        // 초기화 확인
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
    /// 플레이어 움직임 함수 (Update)
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
    /// 카메라 회전 함수 (Late)
    /// </summary>
    private void OnPlayerLook()
    {
        if (!completedInitialize)
            return;

        contoller.OnLook(input.GetLookVector() * sensitivity);
    }
}