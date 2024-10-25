using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_Projectile : Factory<Projectile>
{
    public Projectile SpawnProjectile(Vector3 pos, Quaternion rot, float damage, GameObject owner, Vector3 to, Vector3 from)
    {
        Projectile projectile = SpawnProduct(pos, rot);
        projectile.Init(damage, owner);
        projectile.SetTargetPosition(to, from);

        return projectile;
    }
}