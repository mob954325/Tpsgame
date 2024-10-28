using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    private Factory_Billboard billBoard;
    private Factory_Billboard_Upward billboard_Upward;
    private Factory_Projectile projectile;
    private Factory_Enemy cop;
    private Factory_Enemy cowBoy;

    public Factory_Billboard Billboard { get => billBoard; }
    public Factory_Billboard_Upward Billboard_Upward { get => billboard_Upward; }
    public Factory_Projectile Projectile { get => projectile; }
    public Factory_Enemy Cop { get => cop; }
    public Factory_Enemy CowBoy { get => cowBoy; }


    private void Awake()
    {
        Transform child = transform.GetChild(0);
        billBoard = child.GetComponent<Factory_Billboard>();

        child = transform.GetChild(1);
        billboard_Upward = child.GetComponent<Factory_Billboard_Upward>();

        child = transform.GetChild(2);
        projectile = child.GetComponent<Factory_Projectile>();

        child = transform.GetChild(3);
        cop = child.GetComponent<Factory_Enemy>();

        child = transform.GetChild(4);
        cowBoy = child.GetComponent<Factory_Enemy>();
    }
}