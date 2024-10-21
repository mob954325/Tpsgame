using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIBillboard : MonoBehaviour
{
    TextMeshPro worldText;
    Vector3 LookAtTargetVec;

    private float activeTime = 0f;
    public float maxTime = 3f;

    private void LateUpdate()
    {
        if(activeTime > maxTime) // 타이머 시간 초과 시 비활성화
        {
            transform.gameObject.SetActive(false);
        }

        if(LookAtTargetVec != null)
        { 
            transform.LookAt(LookAtTargetVec);
            transform.position += transform.up * Time.fixedDeltaTime;
            LookAtTargetVec += transform.up * Time.fixedDeltaTime;

            activeTime += Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// 빌보드 바라보는 오브젝트 초기화용 함수
    /// </summary>
    /// <param name="target">바라볼 오브젝트</param>
    public void Init(Vector3 spawnPosition,Transform target)
    {
        transform.position = spawnPosition;
        LookAtTargetVec = target.position;

        Transform child = transform.GetChild(0);
        worldText = child.GetComponent<TextMeshPro>();

        activeTime = 0f;

        transform.gameObject.SetActive(true);
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
