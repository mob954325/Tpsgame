using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private FactoryManager factoryManager;
    public WeaponDataSO data;

    private GameObject ownerObj;
    private Transform shotTransform;
    private Transform shotEffect;

    /// <summary>
    /// 해당 무기 공격력
    /// </summary>
    private float damage = 0f;

    /// <summary>
    /// 사격 사거리
    /// </summary>
    private float shotRange = 10f;

    /// <summary>
    /// 사격 속도
    /// </summary>
    private float fireRate = 1f;

    private int curAmmo = -1;

    /// <summary>
    /// 총알 접근 프로퍼티
    /// </summary>
    public int CurAmmo
    {
        get => curAmmo;
        private set
        {
            curAmmo = Mathf.Clamp(value, 0, data.maxAmmo);
            OnAmmoReduce?.Invoke(value);

            if (curAmmo <= 0)
            {
                CheckCanShot = false;
                StartCoroutine(ReloadCoroutine());
            }
        }
    }

    /// <summary>
    /// 투사체 형태 공격인지 체크하는 변수 (투사체면 true, 히트스캔이면 false)
    /// </summary>
    private bool isProjectile = false;

    /// <summary>
    /// 사격 가능 여부
    /// </summary>
    private bool checkCanShot = false;

    private bool CheckCanShot
    {
        get => checkCanShot;
        set
        {
            checkCanShot = value;

            if(!checkCanShot)
            {
                StartCoroutine(ShotDelayCoroutine());
            }
        }
    }

    /// <summary>
    /// 총알 감소 시 호출되는 델리게이트 (param : 감소 후 남은 총알 개수)
    /// </summary>
    public Action<int> OnAmmoReduce;

    private void Awake()
    {
        factoryManager = FindAnyObjectByType<FactoryManager>();
        shotTransform = transform.GetChild(0);
        shotEffect = shotTransform.GetChild(0);

        Init();
    }

    private void Init()
    {
        damage = data.damage;
        shotRange = data.shotRange;
        fireRate = data.firePerSec;
        isProjectile = data.isProjectile;
        CurAmmo = data.maxAmmo;

        CheckCanShot = true;
    }

    /// <summary>
    /// 무기 주인 오브젝트 설정
    /// </summary>
    /// <param name="obj">IHealth가 있는 오브젝트</param>
    public void SetOwner(GameObject obj)
    {
        ownerObj = obj;
    }

    /// <summary>
    /// 사격 함수
    /// </summary>
    /// <param name="targetVec">사격 여부(true면 사격 시작 / false면 사격 중단)</param>
    public void Shot(bool isShotting, Vector3 targetVec)
    {
        if (!CheckCanShot || !isShotting)
            return;

        if(isProjectile)
        {
            ProjectileShot(targetVec);
        }
        else
        {
            HitScanShot();
        }

        CheckCanShot = false; // 사격 후 사격 비활성화
        CurAmmo--;
    }

    private void HitScanShot()
    {
        RaycastHit hit;
        IHealth objHealth = null;

        Physics.Raycast(shotTransform.position, shotTransform.forward, out hit, shotRange);

        if (hit.transform == null)
            return;

        // 타겟이 적인지 플레이어인지 분리해서 체크
        if (hit.transform.gameObject.layer == 7)
        {
            objHealth = hit.transform.GetComponent<EnemyBase>() as IHealth;
        }
        else if(hit.transform.gameObject.layer == 6)
        {
            objHealth = hit.transform.GetComponent<Player>() as IHealth;
        }

        if(objHealth != null) objHealth.OnHit(damage);
    }
    
    private void ProjectileShot(Vector3 targetVec)
    {
        Vector3 shotPosition = shotEffect.position + transform.forward * 0.1f;
        factoryManager.Projectile.SpawnProjectile(shotPosition, Quaternion.identity, data.damage, ownerObj, targetVec, this.transform.position);
    }

    /// <summary>
    /// 사격 딜레이 코루틴
    /// </summary>
    private IEnumerator ShotDelayCoroutine()
    {
        float timeElapsed = 0f;
        shotEffect.gameObject.SetActive(true);

        while (timeElapsed < fireRate)
        {
            timeElapsed += Time.deltaTime;  // 시간 증가

            if(timeElapsed > 0.1f)          // 0.1초 후 사격 이펙트 종료
            {
                shotEffect.gameObject.SetActive(false);
            }

            yield return null;
        }

        CheckCanShot = true;
    }

    private IEnumerator ReloadCoroutine()
    {
        float timeElapsed = 0f;

        while(timeElapsed < data.reloadTime)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        CurAmmo = data.maxAmmo;
        CheckCanShot = true;
    }
}