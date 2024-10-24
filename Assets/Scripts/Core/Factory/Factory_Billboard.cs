using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_Billboard : Factory<Billboard>
{
    public Billboard SpawnBillboard(Transform lookAtTarget, string str, Vector3? pos = null, Quaternion? rot = null)
    {
        Billboard item = SpawnProduct(pos, rot);
        item.Init(lookAtTarget);
        item.SetText(str);

        return item;
    }
}