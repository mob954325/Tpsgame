using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dummy : EnemyBase
{
    LocalManager localManager;
    EnemyInfoUI enemyInfoUI;

    protected override void Start()
    {
        base.Start();
        localManager = FindAnyObjectByType<LocalManager>();
        enemyInfoUI = FindAnyObjectByType<EnemyInfoUI>();

        // UI init
        enemyInfoUI.SetName(data.objName);
        enemyInfoUI.HpGauge_World.SetGauge(1);
        enemyInfoUI.SetLookAtTarget(localManager.Player.transform);

        OnHitAction += (float damage) => 
        {
            Vector3 spawnPos = this.gameObject.transform.position + Vector3.up * 1.5f;
            FactroyManager.Billboard_Upward.SpawnBillboard(localManager.Player.transform, $"{damage}", spawnPos);
        };
    }

    private void Update()
    {
        RegenHp();
        enemyInfoUI.HpGauge_World.SetGauge(Health / MaxHealth);
    }

    private void RegenHp()
    {
        if (Health < 2)
        {
            Health = MaxHealth;
        }
    }
}