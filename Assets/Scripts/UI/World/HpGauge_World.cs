using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpGauge_World : MonoBehaviour
{
    Transform hpGauge;
    public void Init()
    {
        hpGauge = transform.GetChild(1);
    }

    /// <summary>
    /// 0 에서 1 사이의 hp 비율값을 넣어서 조정하기
    /// </summary>
    public void SetGauge(float hp)
    {
        float value = Mathf.Clamp01(hp);
        hpGauge.localScale = new Vector3(value, 1f, 1f);
    }
}