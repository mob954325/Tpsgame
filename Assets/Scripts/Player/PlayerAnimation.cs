using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    /// <summary>
    /// �÷��̾� x��ǥ �� �ִϸ��̼� �Ķ����
    /// </summary>
    int HashToMove_x = Animator.StringToHash("move_x");

    /// <summary>
    /// �÷��̾� y��ǥ �� �ִϸ��̼� �Ķ����
    /// </summary>
    int HashToMove_y = Animator.StringToHash("move_y");

    /// <summary>
    /// ������Ʈ ���� �ִϸ��̼� �Ķ����
    /// </summary>
    int HashToSprint = Animator.StringToHash("isSprint");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// �ִϸ��̼� ������ �Ķ���� ���� �Լ�
    /// </summary>
    /// <param name="x">x �� �Ķ����</param>
    /// <param name="y">y �� �Ķ����</param>
    public Vector2 SetMoveParam(float x, float y)
    {
        anim.SetFloat(HashToMove_x, x);
        anim.SetFloat(HashToMove_y, y);

        return new Vector2(x, y);
    }

    /// <summary>
    /// �ִϸ��̼� ������Ʈ �Ķ���� ���� �Լ�
    /// </summary>
    /// <param name="value">bool ��</param>
    public bool SetSprintParam(bool value)
    {
        anim.SetBool(HashToSprint, value);

        return value;
    }
}