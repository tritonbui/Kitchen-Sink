using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerOrb : MonoBehaviour
{
    public Transform powerOrbSpawnPoint;
    public GameObject psuedoReceptacle = null;
    public Rigidbody _rb;

    public void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
    }

    public void Respawn()
    {
        transform.position = powerOrbSpawnPoint.position; //moves orb to it's spawn position
        _rb.velocity = Vector3.zero; //resets the velocity, so the ball doesn't slam into the ground (given it was falling for example)
    }

    public void Die()
    {
        if (psuedoReceptacle != null)
        {
            psuedoReceptacle.GetComponent<psuedoReceptacle>().Respawn(); //respawns in psuedoReceptacle given the current orb started in one
        }
        else
        {
            Respawn(); //respawns orb normally given it didn't start in a receptacle
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (LayerMask.NameToLayer("deathBarrier") == col.gameObject.layer)
        {
            Die(); //"kills" the power orb
        }
    }
}
