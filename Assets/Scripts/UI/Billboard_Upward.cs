using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_Upward : Billboard
{
    private float remainTime = 0f;
    private bool isSetTimer = false;

    public override void BillboardUpate()
    {
        CheckIsSetTimer();

        if (remainTime < 0f) // 시간 지나면 파괴
        {
            isSetTimer = false;
            gameObject.SetActive(false);
        }

        base.BillboardUpate();
        transform.position += Vector3.up * Time.fixedDeltaTime;
        transform.eulerAngles = Vector3.up * transform.eulerAngles.y;

        remainTime -= Time.fixedDeltaTime;
    }

    /// <summary>
    /// 빌보드 최대 시간 설정 함수
    /// </summary>
    public void SetTimer(float time)
    {
        remainTime = time;
        isSetTimer = true;
    }

    private void CheckIsSetTimer()
    {
        if (!isSetTimer)
        {
            Debug.LogWarning($"({this.gameObject.name}) 타이머가 설정되지 않았습니다.");
            return;
        }
    }
}