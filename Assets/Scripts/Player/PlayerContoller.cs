using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerContoller : MonoBehaviour
{
    private Rigidbody rb;
    private Transform cameraOffset;
    [SerializeField] private CinemachineVirtualCamera vCam;

    private const float maxVertical = 45f;
    private const float zoomValue = 1f;
    private const float nonZoomValue = 3f;

    /// <summary>
    /// 땅에 닿았는지 확인하는 변수 (false = 점프 중, true = 지상)
    /// </summary>
    private bool isGround = true;

    // 유니티 함수 ================================================================

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cameraOffset = transform.GetChild(0);
    }

    private void Start()
    {
        cameraOffset.eulerAngles = Vector3.zero; // 카메라 오프셋 회전값 초기화
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGround) isGround = true; // 땅에 닿으면 isGround 값 변경
    }


    // 컨트롤 함수 ================================================================

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
        if (cameraOffset.eulerAngles.x > maxVertical && cameraOffset.eulerAngles.x < 180f) // 최대 값 초과시
        {
            cameraOffset.rotation = Quaternion.Euler(maxVertical, 0f, 0f);
        }
        else if (cameraOffset.eulerAngles.x < 360f - maxVertical && cameraOffset.eulerAngles.x > 180f) // 최소 값 초과 시 
        {
            //음수 값이 아닌 360부터 계산됨
            cameraOffset.rotation = Quaternion.Euler(360f - maxVertical, 0f, 0f);
        }
    }

    /// <summary>
    /// 점프 시 실행하는 함수
    /// </summary>
    /// <param name="jumpPower">점프력</param>
    public void OnJump(float jumpPower)
    {
        isGround = false;
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    public void OnZoom(bool isZoom)
    {
        Cinemachine3rdPersonFollow vCamBody = vCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

        if (isZoom)
        {
            vCamBody.CameraDistance = zoomValue;
        }
        else
        {
            vCamBody.CameraDistance = nonZoomValue;
        }
    }

    /// <summary>
    /// isGround 반환 함수 (땅에 닿았는지 확인)
    /// </summary>
    /// <returns>isGround 값</returns>
    public bool GetIsGroundValue()
    {
        return isGround;
    }
}
