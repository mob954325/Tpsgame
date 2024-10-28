#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_05_Factory : TestBase
{
    public Factory_Billboard factory;
    public Factory_Billboard_Upward factory2;
    public Factory_Enemy factory3;
    public Transform lookTarget;
    public Transform spawnPoint;

    private void Start()
    {
        spawnPoint = transform.GetChild(0);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        string str = $"Test Text";
        factory.SpawnBillboard(lookTarget, str, spawnPoint.position, Quaternion.identity);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        string str = $"Test Text2";
        factory2.SpawnBillboard(lookTarget, str, spawnPoint.position, Quaternion.identity);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        factory3.EnemySpawn(spawnPoint.position, Quaternion.identity);
    }
}
#endif