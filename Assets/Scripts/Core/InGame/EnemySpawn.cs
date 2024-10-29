using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    LocalManager localManager;
    Transform[] spawnPosition;

    public float maxTimer = 2f;
    private float timer = -1f;

    private void Awake()
    {
        localManager = GetComponentInParent<LocalManager>();
        spawnPosition = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if (!localManager.IsGameStart)
            return;

        timer += Time.deltaTime;

        if (timer > maxTimer)
        {
            int randPos = UnityEngine.Random.Range(0, spawnPosition.Length);
            EnemyBase curEnemy = localManager.FactoryManager.Cop.EnemySpawn(spawnPosition[randPos].transform.position);
            timer = 0f;
        }
    }
}