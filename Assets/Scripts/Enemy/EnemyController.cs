using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class EnemyController : MonoBehaviour
{
    private Rigidbody rb;
    private Collider coll;
    public NavMeshAgent navAgent;

    public Action OnAttack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void Attack()
    {
        OnAttack?.Invoke();
    }

    /// <summary>
    /// 콜라이더 비활성화 함수
    /// </summary>
    public void DeactiveCollider()
    {
        coll.enabled = false;   
    }
}
