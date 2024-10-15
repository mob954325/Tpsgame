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

    /// <summary>
    /// 움직임 속도
    /// </summary>
    public float movePower = 5f;

    /// <summary>
    /// 점프력
    /// </summary>
    public float jumpPower = 5f;

    /// <summary>
    /// 보기 감도
    /// </summary>
    public float sensitivity = 5f;

    /// <summary>
    /// 달리기 이동속도 증가 배율
    /// </summary>
    public float sprintRatio = 1.2f;

    /// <summary>
    /// 초기화 확인 함수
    /// </summary>
    [SerializeField] private bool completedInitialize = false;

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

    /// <summary>
    /// 사격 시 실행되는 델리게이트
    /// </summary>
    public Action<bool> OnShot;

    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        OnPlayerMove();
        OnPlayerJump();
        OnPlayerShot();
        OnPlayerZoomIn();
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
        if (input == null || contoller == null)
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
        if (!completedInitialize)
            return;

        Vector2 moveVec = input.GetMoveVector();

        // 움직임 처리
        if (anim.SetSprintParam(input.GetSprintValue())) // 달리기 중이면 
        {
            contoller.OnMove(moveVec * movePower * sprintRatio); // 증가 비율 값 만큼 속도 올리기
        }
        else
        {
            contoller.OnMove(moveVec * movePower);
        }

        anim.SetMoveParam(moveVec.x, moveVec.y);
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

    private void OnPlayerJump()
    {
        if (!completedInitialize)
            return;

        bool isGround = contoller.GetIsGroundValue();

        if (input.GetJumpPressValue() && isGround)
        {
            anim.TriggerOnJump();
            contoller.OnJump(jumpPower);
        }

        anim.SetIsGround(isGround);
    }

    private void OnPlayerShot()
    {
        if (!completedInitialize)
            return;

        bool pressed = input.GetShotPressValue();
        if (pressed)
        {
            anim.TriggerOnShot();
            OnShot?.Invoke(pressed);
        }
    }

    private void OnPlayerZoomIn()
    {
        if (!completedInitialize)
            return;

        bool isZoom = input.GetZoomPressValue();
        contoller.OnZoom(isZoom);
    }
}