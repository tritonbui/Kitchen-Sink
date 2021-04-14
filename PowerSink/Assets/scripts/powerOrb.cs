using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerOrb : MonoBehaviour
{
    public Transform powerOrbSpawnPoint;
    public Rigidbody _rb;

    public void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
    }
    public void Respawn()
    {
        transform.position = powerOrbSpawnPoint.position;
        _rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (LayerMask.NameToLayer("deathBarrier") == col.gameObject.layer)
        {
            Respawn();
        }
    }
}
