#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_05_Factory : TestBase
{
    public Factory_Billboard factory;
    public Transform lookTarget;
    public Transform spawnPoint;

    private void Start()
    {
        spawnPoint = transform.GetChild(0);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        factory.SpawnBillboard(lookTarget, spawnPoint.position, Quaternion.identity);
    }
}
#endif