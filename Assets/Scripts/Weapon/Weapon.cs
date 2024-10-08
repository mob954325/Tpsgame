using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WeaponController))]
public class Weapon : MonoBehaviour
{
    private WeaponController controller;

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
}