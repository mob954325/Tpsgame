using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.Animations.Rigging;


[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rig rig;

    /// <summary>
    /// 플레이어 x좌표 값 애니메이션 파라미터
    /// </summary>
    int HashToMove_x = Animator.StringToHash("move_x");

    /// <summary>
    /// 플레이어 y좌표 값 애니메이션 파라미터
    /// </summary>
    int HashToMove_y = Animator.StringToHash("move_y");

    /// <summary>
    /// 스프린트 여부 애니메이션 파라미터
    /// </summary>
    int HashToSprint = Animator.StringToHash("isSprint");

    /// <summary>
    /// 이동 여부 애니메이션 파라미터
    /// </summary>
    int HashToMove = Animator.StringToHash("isMove");

    /// <summary>
    /// 현재 땅에 닿았는지 확인 애니메이션 파라미터
    /// </summary>
    int HashToIsGround = Animator.StringToHash("isGround");

    /// <summary>
    /// 점프 트리거 애니메이션 파라미터
    /// </summary>
    int HashToOnJump = Animator.StringToHash("onJump");

    /// <summary>
    /// 사격 트리거 애니메이션 파라미터
    /// </summary>
    int HashToOnShot = Animator.StringToHash("onShot");

    /// <summary>
    /// 사망 트리거 애니메잇녀 파라미터
    /// </summary>
    int HashToOnDie = Animator.StringToHash("Die");

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rig = GetComponentInChildren<Rig>();
    }

    /// <summary>
    /// 애니메이션 움직임 파라미터 설정 함수
    /// </summary>
    /// <param name="x">x 값 파라미터</param>
    /// <param name="y">y 값 파라미터</param>
    public Vector2 SetMoveParam(float x, float y)
    {
        // 움직임 여부 파라미터 변경
        if (x != 0 || y != 0)
        {
            anim.SetBool(HashToMove, true);
        }
        else
        {
            anim.SetBool(HashToMove, false);
        }

        // x, y 파리미터 변경
        anim.SetFloat(HashToMove_x, x, 0.1f, Time.deltaTime);
        anim.SetFloat(HashToMove_y, y, 0.1f, Time.deltaTime);

        return new Vector2(x, y);
    }

    /// <summary>
    /// 애니메이션 스프린트 파라미터 설정 함수
    /// </summary>
    /// <param name="value">bool 값</param>
    public bool SetSprintParam(bool value)
    {
        anim.SetBool(HashToSprint, value);

        return value;
    }

    /// <summary>
    /// 애니메이션 isGround 파라미터 설정 함수
    /// </summary>
    /// <param name="value">bool 값</param>
    public bool SetIsGround(bool value)
    {
        anim.SetBool(HashToIsGround, value);

        return value;
    }

    /// <summary>
    /// 애니메이션 점프 트리거 작동 함수
    /// </summary>
    public void TriggerOnJump()
    {
        anim.SetTrigger(HashToOnJump);
    }

    /// <summary>
    /// 애니메이션 사격 트리거 작동 함수
    /// </summary>
    public void TriggerOnShot()
    {
        anim.SetTrigger(HashToOnShot);
    }

    /// <summary>
    /// 애니메이션 사망 트리거 작동 함수
    /// </summary>
    public void TriggerOnDie()
    {
        anim.SetTrigger(HashToOnDie);
    }

    /// <summary>
    /// 무기에 리깅된 강도 조절 함수
    /// </summary>
    /// <param name="value">0 - 1 사이의 값</param>
    public void SetRigWeight(float value)
    {
        rig.weight = value;
    }
}