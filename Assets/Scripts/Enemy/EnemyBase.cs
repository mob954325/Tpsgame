using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IHealth
{
    private float health = 0f;

    public float Health 
    {
        get => health;
        set
        {
            health = value;

            if(health < 0f) // 사망
            {
                health = 0f;
                OnDie();
            }
        } 
    }

    private float maxHealth = 10f;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public Action OnHitAction { get; set; }
    public Action OnDieAction { get; set; }

    protected virtual void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// 적 초기화 시 호출되는 함수
    /// </summary>
    protected virtual void Initialize()
    {
        Health = MaxHealth;
    }

    public void OnDie()
    {
        OnDieAction?.Invoke();
        Debug.Log($"{gameObject.name}이(가) 죽었습니다.");        
    }

    public void OnHit(float damageValue)
    {
        Debug.Log($"{gameObject.name}이(가) 데미지를 입었습니다.\n {Health} -> {Health - damageValue}");
        OnHitAction?.Invoke();
        Health -= damageValue;
    }
}