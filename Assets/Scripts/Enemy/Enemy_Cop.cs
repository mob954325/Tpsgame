using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Cop : EnemyBase
{
    private Weapon weapon;
    private Patrol patrol;
    private GameObject target;

    protected override void Start()
    {
        base.Start();

        Transform child = transform.GetChild(1);
        weapon = child.GetComponent<Weapon>();
        weapon.Controller.SetOwner(this.gameObject);

        patrol = FindAnyObjectByType<Patrol>();

        Controller.SetDestination(patrol.GetPatrolPosition()); // navAgent 도착지 초기화
        
        OnHitAction += (float damage) => 
        {
            Vector3 spawnPos = this.gameObject.transform.position + Vector3.up * 1.5f + Vector3.right * 1.5f;
            FManager.Billboard_Upward.SpawnBillboard(target.transform, $"{damage}", spawnPos); 
        };
    }

    private void FixedUpdate()
    {
        if (Health <= 0)
            return;

        GameObject player = GetPlayerInRange();

        // 범위 내에 플레이어가 있다.
        if (player != null)
        {
            StopAllCoroutines();
            Controller.SetStopObject(true);

            target = player;
            Vector3 targetDir = player.transform.position - transform.position;

            Controller.RotateToTarget(targetDir);

            if(IsTargetInSight(targetDir)) // 시야 안에 target 오브젝트가 있으면 공격
            {
                weapon.Controller.Shot(true, target.transform.position);
            }
        }
        else
        {
            target = null; // 타켓 초기화
            Controller.SetStopObject(false);
            if(Controller.CheckReachDestination())
            {
                Vector3 patrolPos = patrol.GetPatrolPosition();
                Controller.SetDestination(patrolPos);
            }
        }
    }

    protected override void BeforeDisable()
    {
        Controller.SetStopObject(true);
        target = null;
    }
}
