using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Projectile : Product
{
    private GameObject owner;

    private float maxActiveTime = 20f;
    private float timer = 0f;
    private float damage = 0f;
    public float force = 5.0f;

    private Vector3 targetDir = Vector3.zero;

    [SerializeField]bool isInstantiate = false;

    Rigidbody rigid;
    TrailRenderer trailRenderer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        trailRenderer.Clear();
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;

        ResetSetting();
    }

    private void FixedUpdate()
    {
        if (!isInstantiate)
            return;

        if(timer > maxActiveTime)
        {
            gameObject.SetActive(false);
        }

        rigid.MovePosition(transform.position + targetDir.normalized * force * Time.fixedDeltaTime);
        timer += Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInstantiate)
            return;

        IHealth target = GetTarget(other.gameObject);

        if (target != null && other.gameObject != owner)
        {
            target.OnHit(damage);
            gameObject.SetActive(false);
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

    private void ResetSetting()
    {
        timer = 0f;
        owner = null;        
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