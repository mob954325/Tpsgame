using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class EnemyController : MonoBehaviour
{
    private Rigidbody rb;
    private Collider coll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    /// <summary>
    /// 콜라이더 비활성화 함수
    /// </summary>
    public void DeactiveCollider()
    {
        coll.enabled = false;   
    }
}
