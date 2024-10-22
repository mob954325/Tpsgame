using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private TextMeshPro worldText;
    protected Transform lookAtTarget;

    protected void LateUpdate()
    {
        BillboardUpate();
    }

    /// <summary>
    /// 빌보드 바라보는 오브젝트 초기화용 함수
    /// </summary>
    /// <param name="target">바라볼 오브젝트</param>
    public void Init(Vector3 spawnPosition,Transform target)
    {
        transform.position = spawnPosition;
        lookAtTarget = target;

        Transform child = transform.GetChild(0);
        worldText = child.GetComponent<TextMeshPro>();

        transform.gameObject.SetActive(true);
    }

    /// <summary>
    /// 빌보드 업데이트 함수
    /// </summary>
    public virtual void BillboardUpate()
    {
        transform.LookAt(lookAtTarget);
    }

    /// <summary>
    /// 텍스트 설정 함수 (문자열)
    /// </summary>
    /// <param name="str">설정할 문자열</param>
    public void SetText(string str)
    {
        worldText.text = str;
    }

    /// <summary>
    /// 텍스트 설정 함수 (int형)
    /// </summary>
    /// <param name="value">int형 값</param>
    public void SetText(int value)
    {
        worldText.text = $"{value}";
    }
}
