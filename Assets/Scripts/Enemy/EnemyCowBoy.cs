using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCowBoy : EnemyBase
{
    private Weapon weapon;
    private WeaponDataSO weaponData;
    public GameObject target;

    protected override void Start()
    {
        base.Start();

        weapon = transform.GetChild(0).GetChild(0).GetComponent<Weapon>();
        weaponData = weapon.Controller.data;

        weapon.Init(this.gameObject);

        Controller.OnAttack += () => { weapon.Controller.Shot(true, target.transform.position); };
    }

    private void Update()
    {
        if(target != null && CheckInSight(target.transform.position))
        {
            Controller.Attack();
        }
    }
}
