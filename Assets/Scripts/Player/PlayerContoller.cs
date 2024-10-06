using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerContoller : MonoBehaviour
{
    private Rigidbody rb;
    private Transform cameraOffset;

    private const float maxVertical = 45f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cameraOffset = transform.GetChild(0);
    }

    /// <summary>
    /// 플레이어 움직임 함수
    /// </summary>
    /// <param name="input">움직임 벡터 값</param>
    public void OnMove(Vector2 input)
    {
        rb.MovePosition((transform.forward * input.y + transform.right * input.x) * Time.fixedDeltaTime + transform.position);
    }

    /// <summary>
    /// 플레이어 마우스 움직임 함수
    /// </summary>
    /// <param name="input">마우스 움직임 벡터</param>
    public void OnLook(Vector2 input)
    {
        // 좌우 바라보기
        rb.MoveRotation(Quaternion.Euler(transform.eulerAngles + input.x * Vector3.up * Time.fixedDeltaTime));


        // 위 아래 바라보기 (카메라만 회전)
        cameraOffset.rotation *= Quaternion.AngleAxis(input.y * Time.fixedDeltaTime, Vector3.left);

        // 회전 값 초과시 회전 값 처리 if문
        if(cameraOffset.eulerAngles.x > 45f && cameraOffset.eulerAngles.x < 180f) // 최대 값 초과시
        {
            cameraOffset.rotation = Quaternion.Euler(maxVertical, cameraOffset.eulerAngles.y, cameraOffset.eulerAngles.z);
        }
        else if (cameraOffset.eulerAngles.x < 360f - maxVertical && cameraOffset.eulerAngles.x > 180f) // 최소 값 초과 시 
        {
            //음수 값이 아닌 360부터 계산됨
            cameraOffset.rotation = Quaternion.Euler(360f - maxVertical, cameraOffset.eulerAngles.y, cameraOffset.eulerAngles.z);
        }
    }
}
