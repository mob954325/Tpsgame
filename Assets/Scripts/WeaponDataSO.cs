using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData_", menuName = "ScriptableObject/WeaponData", order = 0)]
public class WeaponDataSO : ScriptableObject
{
    /// <summary>
    /// 무기 이름
    /// </summary>
    public string weaponName = "[EmptyWeaponName]";

    /// <summary>
    /// 무기 데미지
    /// </summary>
    public float damage = 1f;

    /// <summary>
    /// 사거리
    /// </summary>
    public float shotRange = 50f;

    /// <summary>
    /// 초당 공격 속도
    /// </summary>
    public float firePerSec = 0.3f;

    /// <summary>
    /// 투사체인지 확인하는 변수
    /// </summary>
    public bool isProjectile = false;
}