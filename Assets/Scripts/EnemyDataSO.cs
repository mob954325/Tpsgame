using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData_", menuName = "ScriptableObject/EnemyData", order = 1)]
public class EnemyDataSO : ScriptableObject
{
    /// <summary>
    /// 적 이름
    /// </summary>
    public string objName = "[EnmptyEnemyName]";

    /// <summary>
    /// 최대 체력
    /// </summary>
    public float maxHealth = 3f;

    /// <summary>
    /// 데미지
    /// </summary>
    public float damage = 1f;

    /// <summary>
    /// 움직임 속도
    /// </summary>
    public float moveSpeed = 1f;
}
