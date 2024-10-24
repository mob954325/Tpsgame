using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_Billboard_Upward : Factory<Billboard_Upward>
{
    public Billboard_Upward SpawnBillboard(Transform lookAtTarget, string str, Vector3? pos = null, Quaternion? rot = null)
    {
        Billboard_Upward item = SpawnProduct(pos, rot);
        item.Init(lookAtTarget);
        item.SetTimer(3f);
        item.SetText(str);

        return item;
    }
}