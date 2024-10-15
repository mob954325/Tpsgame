using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimation), typeof(EnemyController))]
public abstract class EnemyBase : MonoBehaviour, IHealth
{
    public EnemyDataSO data;
    private EnemyAnimation anim;
    private EnemyController controller;

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

    private const float disbleDelayTime = 3f;

    public Action OnHitAction { get; set; }

    public Action OnDieAction { get; set; }

    // 유니티 ========================================================================

    protected virtual void Start()
    {
        Initialize();
        Health = 1f;
    }

    protected virtual void OnDisable()
    {
        OnDieAction -= anim.TriggerOnDead;
        OnDieAction -= controller.DeactiveCollider;
    }

    // 기능 ========================================================================

    /// <summary>
    /// 적 초기화 시 호출되는 함수
    /// </summary>
    protected virtual void Initialize()
    {
        anim = GetComponent<EnemyAnimation>();
        controller = GetComponent<EnemyController>();

        OnDieAction += anim.TriggerOnDead;
        OnDieAction += controller.DeactiveCollider;

        MaxHealth = data.maxHealth;
        Health = MaxHealth;
    }

    // IHealth ========================================================================

    public void OnDie()
    {
        OnDieAction?.Invoke();
        StartCoroutine(OnDieCoroutine());
        Debug.Log($"{gameObject.name}이(가) 죽었습니다.");        
    }

    /// <summary>
    /// 사망 코루틴
    /// </summary>
    private IEnumerator OnDieCoroutine()
    {
        float timeElapsed = 0.0f;

        while(timeElapsed < disbleDelayTime)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        BeforeDisable();
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 비활성화 전 실행하는 함수
    /// </summary>
    protected virtual void BeforeDisable()
    {

    }

    public void OnHit(float damageValue)
    {
        Debug.Log($"{gameObject.name}이(가) 데미지를 입었습니다.\n {Health} -> {Health - damageValue}");
        Health -= damageValue;
        OnHitAction?.Invoke();
    }
}