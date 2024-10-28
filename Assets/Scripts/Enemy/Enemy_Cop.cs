using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Cop : EnemyBase
{
    private LocalManager localManager;
    private EnemyInfoUI enemyInfoUI;

    private Weapon weapon;
    private Patrol patrol;
    private GameObject target;

    protected override void Start()
    {
        localManager = FindAnyObjectByType<LocalManager>();
        Transform child = transform.GetChild(1);
        weapon = child.GetComponent<Weapon>();
        child = transform.GetChild(2);
        enemyInfoUI = child.GetComponent<EnemyInfoUI>();    
        patrol = FindAnyObjectByType<Patrol>();

        base.Start();
        
        OnHitAction += (float damage) => 
        {
            Vector3 spawnPos = this.gameObject.transform.position + Vector3.up * 1.5f;
            FactroyManager.Billboard_Upward.SpawnBillboard(localManager.Player.transform, $"{damage}", spawnPos); 
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
                weapon.Controller.Shot(true, target.transform.position + Vector3.up);
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

        enemyInfoUI.HpGauge_World.SetGauge(Health / MaxHealth);
    }

    protected override void Init()
    {
        base.Init();

        enemyInfoUI.SetName(data.objName);
        enemyInfoUI.HpGauge_World.SetGauge(1);
        enemyInfoUI.SetLookAtTarget(localManager.Player.transform);
        weapon.Controller.SetOwner(this.gameObject);
        Controller.SetDestination(patrol.GetPatrolPosition()); // navAgent 도착지 초기화
    }

    protected override void BeforeDisable()
    {
        Controller.SetStopObject(true);
        target = null;
    }
}
