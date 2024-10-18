using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(PlayerInput), typeof(PlayerContoller), typeof(PlayerAnimation))]
public class Player : MonoBehaviour, IHealth
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

    public float Health { get; set; }

    public float MaxHealth { get; set; }

    public Action OnHitAction { get; set; }

    public Action OnDieAction { get; set; }

    /// <summary>
    /// 사격 시 실행되는 델리게이트
    /// </summary>
    public Action<bool> OnWeaponShot;

    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {

    }

    /// <summary>
    /// Player 초기화 함수
    /// </summary>
    private void Init()
    {
        input = GetComponent<PlayerInput>();
        contoller = GetComponent<PlayerContoller>();
        anim = GetComponent<PlayerAnimation>();

        input.OnMove += OnPlayerMove;
        input.OnLook += OnPlayerLook;
        input.OnJump += OnPlayerJump;
        input.OnShot += OnPlayerShot;
        input.OnZoomIn += OnPlayerZoomIn;

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
    private void OnPlayerMove(Vector2 moveVec, bool isSprint)
    {
        if (!completedInitialize)
            return;

        // 움직임 처리
        if(isSprint) 
        {
            // 달리기 
            contoller.OnMove(moveVec * movePower * sprintRatio); // 증가 비율 값 만큼 속도 올리기
            anim.SetSprintParam(isSprint);
        }
        else
        {
            // 걷기
            contoller.OnMove(moveVec * movePower);
        }

        anim.SetMoveParam(moveVec.x, moveVec.y);
    }

    /// <summary>
    /// 카메라 회전 함수 (Late)
    /// </summary>
    private void OnPlayerLook(Vector2 lookVec)
    {
        if (!completedInitialize)
            return;

        contoller.OnLook(lookVec * sensitivity);
    }

    private void OnPlayerJump(bool isJump)
    {
        if (!completedInitialize)
            return;

        bool isGround = contoller.GetIsGroundValue();

        if (isJump && isGround)
        {
            anim.TriggerOnJump();
            contoller.OnJump(jumpPower);
        }

        anim.SetIsGround(isGround);
    }

    private void OnPlayerShot(bool isShot)
    {
        if (!completedInitialize)
            return;

        if (isShot)
        {
            anim.TriggerOnShot();
            OnWeaponShot?.Invoke(isShot);
        }
    }

    private void OnPlayerZoomIn(bool isZoomIn)
    {
        if (!completedInitialize)
            return;

        contoller.OnZoom(isZoomIn);
    }

    // IHealth ======================================

    public void OnHit(float damageValue)
    {
        
    }

    public void OnDie()
    {
        
    }
}