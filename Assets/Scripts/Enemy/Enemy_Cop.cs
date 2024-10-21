using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Cop : EnemyBase
{
    /// <summary>
    /// 목표 게임 오브젝트
    /// </summary>
    GameObject target;

    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        GameObject player = GetPlayerInRange();

        // 범위 내에 플레이어가 있다.
        if (player != null)
        {
            Vector3 targetDir = player.transform.position - transform.position;

            Controller.RotateToTarget(targetDir);

            if(IsTargetInSight(targetDir))
            {
                Debug.Log("사격");
            }
        }

    }
}
