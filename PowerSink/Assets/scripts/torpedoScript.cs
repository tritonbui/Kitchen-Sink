using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torpedoScript : MonoBehaviour
{
    private Rigidbody _rb;
    public Transform target;
    private Vector3 targetDir;
    private Vector3 targetVel;
    public float speed = 2f;
    public float deathTimer = 10f;

    public void Start()
    {
        this._rb = this.GetComponent<Rigidbody>();

        targetDir = target.position - this.transform.position;

        targetVel = targetDir * speed;

        this.gameObject.transform.rotation = Quaternion.LookRotation(targetDir, Vector3.up);

        Destroy(this.gameObject, deathTimer);
    }

    public void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        _rb.velocity = targetVel;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (!col.isTrigger || LayerMask.NameToLayer("deathBarrier") == col.gameObject.layer)
        {
            Destroy(this.gameObject);
        }
    }
}
