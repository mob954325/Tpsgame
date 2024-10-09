using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WeaponController))]
public class Weapon : MonoBehaviour
{
    private Player player;
    private WeaponController controller;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        controller = GetComponent<WeaponController>();

        player.OnShot += controller.Shot;
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
}