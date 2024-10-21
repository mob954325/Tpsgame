#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_04_Billboard : TestBase
{
    public UIBillboard billboard;
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
            billboard.Init(spawnPoint.position, lookAtTarget);
        }
    }
}
#endif