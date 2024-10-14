using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor.Timeline;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody))]
public class PlayerContoller : MonoBehaviour
{
    private Rigidbody rb;
    private Transform cameraOffset;
    [SerializeField] private CinemachineVirtualCamera vCam;

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
        float inputX = input.x * Time.fixedDeltaTime;
        rb.MoveRotation(Quaternion.Euler(transform.eulerAngles + Vector3.up * inputX));

        LimitCamPosition(input);
    }

    private void LimitCamPosition(Vector2 input)
    {
        Vector3 camRot = cameraOffset.eulerAngles + input.y * Time.fixedDeltaTime * Vector3.left;
        camRot.x = camRot.x > 180 ? camRot.x - 360 : camRot.x;
        camRot.x = Mathf.Clamp(camRot.x, -45f, 45f);

        cameraOffset.rotation = Quaternion.Euler(camRot);
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
