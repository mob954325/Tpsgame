using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

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

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// 애니메이션 움직임 파라미터 설정 함수
    /// </summary>
    /// <param name="x">x 값 파라미터</param>
    /// <param name="y">y 값 파라미터</param>
    public Vector2 SetMoveParam(float x, float y)
    {
        anim.SetFloat(HashToMove_x, x);
        anim.SetFloat(HashToMove_y, y);

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
}