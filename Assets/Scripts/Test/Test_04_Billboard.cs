#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_04_Billboard : TestBase
{
    public Billboard normal;
    public Billboard_Upward upward;

    public Transform lookAtTarget;
    private Transform spawnPoint;

    private void Start()
    {
        spawnPoint = transform.GetChild(0);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        if(lookAtTarget != null)
        {
            normal.Init(lookAtTarget);
        }
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        if (lookAtTarget != null)
        {
            upward.SetTimer(3f);
            upward.Init(lookAtTarget);
        }
    }
}
#endif