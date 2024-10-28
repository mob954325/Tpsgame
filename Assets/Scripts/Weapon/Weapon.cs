using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WeaponController))]
public class Weapon : MonoBehaviour
{
    private GameObject owner;
    private WeaponController controller;

    private void Awake()
    {
        controller = GetComponent<WeaponController>();
    }

    public void Init(GameObject owner)
    {
        this.owner = owner;
        controller.SetOwner(owner);
    }

    /// <summary>
    /// WeaponController 접근용 프로퍼티
    /// </summary>
    public WeaponController Controller
    {
        get
        {
            if(controller == null)
            {
                controller = GetComponent<WeaponController>();
            }

            return controller;
        }
    }

    public int GetMaxAmmo()
    {
        return Controller.data.maxAmmo;
    }
}