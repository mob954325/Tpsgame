using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.VFX;



#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(EnemyAnimation), typeof(EnemyController))]
public abstract class EnemyBase : MonoBehaviour, IHealth
{
    public EnemyDataSO data;
    private EnemyAnimation anim;
    private EnemyController controller;
    private FactroyManager factoryManager;

    protected FactroyManager FactroyManager { get => factoryManager; }

    /// <summary>
    /// EnmeyController 접근용 프로퍼티
    /// </summary>
    public EnemyController Controller { get => controller; }

    private float health = 0f;

    public float Health 
    {
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0f, MaxHealth);

            if(health == 0f) // 사망
            {
                health = 0f;
                OnDie();
            }
        } 
    }

    private float maxHealth = 10f;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    /// <summary>
    /// 비활성화 딜레이 시간
    /// </summary>
    private const float disbleDelayTime = 3f;

    public Action<float> OnHitAction { get; set; }

    public Action OnDieAction { get; set; }

    // 유니티 ========================================================================

    protected virtual void Start()
    {
        Init();
    }

    protected virtual void OnDisable()
    {
        OnDieAction -= anim.TriggerOnDead;
        OnDieAction -= controller.DeactiveCollider;
    }

    // 기능 ========================================================================

    /// <summary>
    /// 적 초기화 시 호출되는 함수 (awake 이후 실행)
    /// </summary>
    protected virtual void Init()
    {
        factoryManager = FindAnyObjectByType<FactroyManager>();

        anim = GetComponent<EnemyAnimation>();
        controller = GetComponent<EnemyController>();
        controller.Init(data);

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
        if (health <= 0)
            return;

        Health -= damageValue;
        OnHitAction?.Invoke(damageValue);
    }

    /// <summary>
    /// 범위 내 플레이어 오브젝트 찾기
    /// </summary>
    /// <returns></returns>
    protected GameObject GetPlayerInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, data.attackRange);
        GameObject result = null;
        int curLayer = 1;

        foreach (Collider coll in colliders)
        {
            curLayer = coll.gameObject.layer;
            if (1 << curLayer == LayerMask.GetMask("Player"))
            {
                result = coll.gameObject;
            }
        }
        return result;
    }

    /// <summary>
    /// 파라미터의 타겟 위치가 시야 범위 내에 있는지 확인하는 함수
    /// </summary>
    /// <param name="toTargetDir">타겟 위치</param>
    /// <returns>범위 안에 있으면 true 아니면 false</returns>
    protected bool IsTargetInSight(Vector3 toTargetDir)
    {
        float angle = Vector3.Angle(transform.forward, toTargetDir);

        return angle < data.attackAngle;
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (data == null)
            return;

        float angle = data.attackAngle;
        float range = data.attackRange;

        Vector3 forward = transform.forward * range;
        Quaternion p1 = Quaternion.AngleAxis(angle, transform.up);
        Quaternion p2 = Quaternion.AngleAxis(-angle, transform.up);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + forward);

        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, transform.up, data.attackRange, 2f);

        Handles.color = Color.red;
        Handles.DrawLine(transform.position, transform.position + p1 * forward, 2f);
        Handles.DrawLine(transform.position, transform.position + p2 * forward, 2f);

        Handles.color = Color.red;
        Handles.DrawWireArc(transform.position, transform.up, p2 * forward, angle * 2, range, 2f);
    }
#endif
}