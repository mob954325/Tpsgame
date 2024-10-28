using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


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
    /// 최대 총알 수 
    /// </summary>
    public int maxAmmo = 10;

    /// <summary>
    /// 재장전 시간
    /// </summary>
    public float reloadTime = 1f;

    /// <summary>
    /// 투사체인지 확인하는 변수
    /// </summary>
    public bool isProjectile = false;

    /// <summary>
    /// 투사체 오브젝트
    /// </summary>
    [HideInInspector]
    public GameObject projectileObj = null;
}

#if UNITY_EDITOR
[CustomEditor(typeof(WeaponDataSO))]
public class WeaponDataSO_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WeaponDataSO script = (WeaponDataSO)target;

        if(script.isProjectile)
        {
            script.projectileObj = EditorGUILayout.ObjectField("Projecttile", script.projectileObj, typeof(GameObject), true) as GameObject;
        }
    }
}
#endif