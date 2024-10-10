using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Transform shotTransform;

    /// <summary>
    /// 해당 무기 공격력
    /// </summary>
    [Tooltip("해당 무기 공격력")]
    public float damage = 1f;

    /// <summary>
    /// 사격 사거리
    /// </summary>
    public float shotRange = 50f;

    /// <summary>
    /// 사격 속도
    /// </summary>
    public float fireRate = 1.0f;

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
        CheckCanShot = false;
    }

    /// <summary>
    /// 사격 함수
    /// </summary>
    /// <param name="isShotting">사격 여부(true면 사격 시작 / false면 사격 중단)</param>
    public void Shot(bool isShotting)
    {
        if (!CheckCanShot || !isShotting)
            return;

        RaycastHit hit;

        if(Physics.Raycast(shotTransform.position, shotTransform.forward, out hit, shotRange, LayerMask.GetMask("Enemy")))
        {
            // 피격 성공 시 
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            Debug.Log("Did Hit");

            IHealth objHealth = hit.transform.GetComponent<Enemy>() as IHealth;
            objHealth.OnHit(damage);
        }
        else // 빗맞춤
        {
            Debug.DrawRay(transform.position, transform.forward * 1000, Color.white);
        }

        CheckCanShot = false; // 사격 후 사격 비활성화
    }

    /// <summary>
    /// 사격 딜레이 코루틴
    /// </summary>
    private IEnumerator ShotDelayCoroutine()
    {
        float timeElapsed = 0f;
        while(timeElapsed < fireRate)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        CheckCanShot = true;
    }
}