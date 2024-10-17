#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_02_FireRange : TestBase
{
    public Weapon curWeapon;
    public GameObject projectileObj;
    private Transform spawnPosition;

    public float setTimeScale = 0.5f;

    private void Start()
    {
        spawnPosition = transform.GetChild(0).transform;
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        curWeapon.Controller.Shot(context.performed);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Time.timeScale = setTimeScale;
        GameObject obj = Instantiate(projectileObj);
        obj.transform.position = spawnPosition.position;
        
        Projectile script = obj.GetComponent<Projectile>();
        script.Init(1f);        
    }
}
#endif