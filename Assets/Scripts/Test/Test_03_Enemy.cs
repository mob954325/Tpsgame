#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Test_03_Enemy : TestBase
{
    public NavMeshAgent nav;
    private Transform target;

    private void Start()
    {
        target = transform.GetChild(0);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        if(nav == null)
            return;

        nav.SetDestination(target.position);
    }
}
#endif