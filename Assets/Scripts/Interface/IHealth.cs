using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 체력 인터페이스
/// </summary>
public interface IHealth
{
    /// <summary>
    /// 현재 체력
    /// </summary>
    public float Health { get; set; } 

    /// <summary>
    /// 최대 체력
    /// </summary>
    public float MaxHealth { get; set; }


    /// <summary>
    /// 피격 시 호출되는 델리게이트
    /// </summary>
    public Action<float> OnHitAction { get; set; }

    /// <summary>
    /// 사망 시 호출되는 델리게이트
    /// </summary>
    public Action OnDieAction { get; set; }

    /// <summary>
    /// 피격 시 호출되는 함수
    /// </summary>
    /// <param name="damageValue">받는 데미지</param>
    public void OnHit(float damageValue);

    /// <summary>
    /// 사망시 호출되는 함수
    /// </summary>
    public void OnDie();
}