using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Cop : EnemyBase
{
    private Weapon weapon;
    private GameObject target;

    protected override void Start()
    {
        base.Start();
        weapon = GetComponentInChildren<Weapon>();
        weapon.Controller.SetOwner(this.gameObject);
    }

    private void FixedUpdate()
    {
        GameObject player = GetPlayerInRange();

        // 범위 내에 플레이어가 있다.
        if (player != null)
        {
            target = player;
            Vector3 targetDir = player.transform.position - transform.position;

            Controller.RotateToTarget(targetDir);

            if(IsTargetInSight(targetDir))
            {
                weapon.Controller.Shot(true, target.transform.position);
            }
        }
        else
        {
            target = null; // 타켓 초기화
        }
    }
}
