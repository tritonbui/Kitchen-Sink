using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class playerRespawn : MonoBehaviour
{
    public playerInteraction _pi;
    public playerMovement _pm;
    private Rigidbody _rb;
    public Transform playerSpawnPoint;
    public riggedDiverScript _rds;
    public GameObject[] toggleSwitches;

    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public void Respawn() //respawns player and resets necessary values
    {
        GameObject _pf;
        _pf = GameObject.Find("playerFollow");

        GameObject _cam;
        _cam = GameObject.Find("Main Camera");

        _cam.transform.position = playerSpawnPoint.position + new Vector3(0, 5, -17);
        _pf.transform.position = playerSpawnPoint.position;
        transform.position = playerSpawnPoint.position;
        _rb.velocity = Vector3.zero;

        if (_pi.heldPowerOrb != null)
        {
            _pi.heldPowerOrb.SetActive(true);
            _pi.heldPowerOrb.GetComponent<powerOrb>().Die();
            _pi.heldPowerOrb = null;
            _pi.hasPowerOrb = false;
            _rds.hasNothing();
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (LayerMask.NameToLayer("deathBarrier") == col.gameObject.layer)
        {
            Respawn();
        }

        if (col.gameObject.tag == "checkpoint" && _pm.isGrounded) //changes player spawn point to checkpoint location on contact (also requires player is grounded)
        {
            playerSpawnPoint.position = col.gameObject.transform.position;
        }
    }
}
