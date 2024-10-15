using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Transform shotTransform;
    private Transform shotEffect;
    public WeaponDataSO data;

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

    private void Awake()
    {
        shotTransform = transform.GetChild(0);
        shotEffect = shotTransform.GetChild(0);

        CheckCanShot = false;
        Init();
    }

    private void Init()
    {
        damage = data.damage;
        shotRange = data.shotRange;
        fireRate = data.firePerSec;
        isProjectile = data.isProjectile;
    }

    /// <summary>
    /// 사격 함수
    /// </summary>
    /// <param name="isShotting">사격 여부(true면 사격 시작 / false면 사격 중단)</param>
    public void Shot(bool isShotting)
    {
        if (!CheckCanShot || !isShotting)
            return;

        HitScanShot();

        CheckCanShot = false; // 사격 후 사격 비활성화
    }

    private void HitScanShot()
    {
        RaycastHit hit;

        if (Physics.Raycast(shotTransform.position, shotTransform.forward, out hit, shotRange, LayerMask.GetMask("Enemy")))
        {
            // 피격 성공 시 
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            Debug.Log("Did Hit");

            IHealth objHealth = hit.transform.GetComponent<EnemyBase>() as IHealth;
            objHealth.OnHit(damage);
        }
        else // 빗맞춤
        {
            Debug.DrawRay(transform.position, transform.forward * 1000, Color.white);
        }
    }

    private void ProjectileShot()
    {

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
}