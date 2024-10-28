using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_Enemy : Factory<EnemyBase>
{
    private LocalManager manager;

    private void Start()
    {
    }

    public EnemyBase EnemySpawn(Vector3? pos = null, Quaternion? rot = null, float score = 1f)
    {
        EnemyBase enemy = SpawnProduct(pos, rot);
/*        enemy.OnDeactive += () => 
        {
            manager = FindAnyObjectByType<LocalManager>();
            manager.SetScore(score); 
        };*/

        return enemy;
    }
}
