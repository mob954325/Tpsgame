using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyInfoUI : MonoBehaviour
{
    private HpGauge_World hpGauge;
    private TextMeshPro objNameText;
    private Transform lookAtTarget;

    public HpGauge_World HpGauge_World { get => hpGauge; }

    private void Awake()
    {
        objNameText = GetComponentInChildren<TextMeshPro>();
        hpGauge = GetComponentInChildren<HpGauge_World>();
    }

    private void FixedUpdate()
    {
        if (lookAtTarget == null)
            return;

        transform.LookAt(lookAtTarget);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
    }

    public void SetLookAtTarget(Transform target)
    {
        lookAtTarget = target;
    }

    public void SetName(string name)
    {
        objNameText.text = name;
    }
}