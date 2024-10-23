using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_Billboard : Factory<Billboard>
{
    private void Awake()
    {
        Init();
    }

    public void SpawnBillboard(Transform lookAtTarget, Vector3? pos = null, Quaternion? rot = null)
    {
        Billboard item = SpawnProduct(pos, rot);
        item.Init(lookAtTarget);
    }
}