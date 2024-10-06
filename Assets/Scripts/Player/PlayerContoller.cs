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
    /// �÷��̾� ������ �Լ�
    /// </summary>
    /// <param name="input">������ ���� ��</param>
    public void OnMove(Vector2 input)
    {
        rb.MovePosition((transform.forward * input.y + transform.right * input.x) * Time.fixedDeltaTime + transform.position);
    }

    /// <summary>
    /// �÷��̾� ���콺 ������ �Լ�
    /// </summary>
    /// <param name="input">���콺 ������ ����</param>
    public void OnLook(Vector2 input)
    {
        // �¿� �ٶ󺸱�
        rb.MoveRotation(Quaternion.Euler(transform.eulerAngles + input.x * Vector3.up * Time.fixedDeltaTime));


        // �� �Ʒ� �ٶ󺸱� (ī�޶� ȸ��)
        cameraOffset.rotation *= Quaternion.AngleAxis(input.y * Time.fixedDeltaTime, Vector3.left);

        // ȸ�� �� �ʰ��� ȸ�� �� ó�� if��
        if(cameraOffset.eulerAngles.x > 45f && cameraOffset.eulerAngles.x < 180f) // �ִ� �� �ʰ���
        {
            cameraOffset.rotation = Quaternion.Euler(maxVertical, cameraOffset.eulerAngles.y, cameraOffset.eulerAngles.z);
        }
        else if (cameraOffset.eulerAngles.x < 360f - maxVertical && cameraOffset.eulerAngles.x > 180f) // �ּ� �� �ʰ� �� 
        {
            //���� ���� �ƴ� 360���� ����
            cameraOffset.rotation = Quaternion.Euler(360f - maxVertical, cameraOffset.eulerAngles.y, cameraOffset.eulerAngles.z);
        }
    }
}
