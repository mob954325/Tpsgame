using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    Animator anim;

    int hashToDie = Animator.StringToHash("Die");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.enabled = true;
    }

    /// <summary>
    /// 애니메이션 사망 트리거 함수
    /// </summary>
    public void TriggerOnDead()
    {
        anim.SetTrigger(hashToDie);
    }
}
