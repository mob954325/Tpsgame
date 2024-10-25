using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactroyManager : MonoBehaviour
{
    private Factory_Billboard billBoard;
    private Factory_Billboard_Upward billboard_Upward;
    private Factory_Projectile projectile;

    public Factory_Billboard Billboard { get => billBoard; }
    public Factory_Billboard_Upward Billboard_Upward { get => billboard_Upward; }
    public Factory_Projectile Projectile { get => projectile; }

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        billBoard = child.GetComponent<Factory_Billboard>();

        child = transform.GetChild(1);
        billboard_Upward = child.GetComponent<Factory_Billboard_Upward>();

        child = transform.GetChild(2);
        projectile = child.GetComponent<Factory_Projectile>();
    }
}