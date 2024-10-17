using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private float damage = 0f;
    public float force = 5.0f;

    [SerializeField]bool isInstantiate = false;

    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!isInstantiate)
            return;

        rigid.MovePosition(transform.position + transform.forward * force * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInstantiate)
            return;

        IHealth target = other.gameObject.GetComponent<EnemyBase>() as IHealth;

        if (target != null)
        {
            target.OnHit(damage);
        }

        Destroy(this.gameObject);
    }
    public void Init(float damageData)
    {
        damage = damageData;
        isInstantiate = true;
    }
}