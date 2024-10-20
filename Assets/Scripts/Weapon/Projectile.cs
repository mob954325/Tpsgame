using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private GameObject owner;

    private float damage = 0f;
    public float force = 5.0f;

    private Vector3 targetDir = Vector3.zero;

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

        rigid.MovePosition(transform.position + targetDir.normalized * force * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInstantiate)
            return;

        IHealth target = GetTarget(other.gameObject);

        if (target != null && other.gameObject != owner)
        {
            target.OnHit(damage);
            Destroy(this.gameObject);
        }
    }

    public void Init(float damageData, GameObject curOwner = null)
    {
        damage = damageData;
        owner = curOwner;

        isInstantiate = true;
    }

    public void SetTargetPosition(Vector3 to, Vector3 from)
    {
        targetDir = to - from;
    }

    private IHealth GetTarget(GameObject obj)
    {
        IHealth result = null;

        if(obj.layer == 6)
        {
            result = obj.GetComponent<Player>() as IHealth;
        }
        else if(obj.layer == 7)
        {
            result = obj.GetComponent<EnemyBase>() as IHealth;
        }

        return result;
    }
}