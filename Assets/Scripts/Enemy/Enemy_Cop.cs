using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Cop : EnemyBase
{
    protected override void Start()
    {
        base.Start();
        Controller.SetDestination(Vector3.zero);
    }
}
