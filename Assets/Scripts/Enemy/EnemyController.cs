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
    private NavMeshAgent navAgent;

    public Action OnAttack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// 목적기 설정 함수
    /// </summary>
    /// <param name="value">위치 벡터</param>
    public void SetDestination(Vector3 value)
    {
        navAgent.destination = value;
    }

    /// <summary>
    /// 오브젝트를 멈출지 설정하는 함수
    /// </summary>
    /// <param name="value">true면 정지 false 목표로 이동</param>
    public bool SetStopObject(bool value)
    {
        navAgent.isStopped = value;
        return value;   
    }


    public void RotateToTarget(Vector3 toTargetDir)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              Quaternion.LookRotation(toTargetDir), 
                                              Time.deltaTime * 5f);
    }


    /// <summary>
    /// 목적지에 도달했는지 확인하는 함수
    /// </summary>
    /// <returns>도달했으면 true 아니면 false</returns>
    public bool CheckReachDestination()
    {
        float length = navAgent.destination.sqrMagnitude;

        return length < navAgent.stoppingDistance; 
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
