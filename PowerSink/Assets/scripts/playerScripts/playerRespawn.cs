using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class playerRespawn : MonoBehaviour
{
    public playerInteraction _pi;
    private Rigidbody _rb;
    public Transform playerSpawnPoint;

    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public void Respawn()
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
            GameManager._instance.gameUI.putDownOrb();
            _pi.heldPowerOrb.SetActive(true);
            _pi.heldPowerOrb.GetComponent<powerOrb>().Die();
            _pi.heldPowerOrb = null;
            _pi.hasPowerOrb = false;
        }

        if (_pi.touchedToggleSwitch != null)
        {
            _pi.touchedToggleSwitch.GetComponent<toggleSwitch>().StateA();
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (LayerMask.NameToLayer("deathBarrier") == col.gameObject.layer)
        {
            Respawn();
        }

        if (col.gameObject.tag == "checkpoint")
        {
            playerSpawnPoint.position = col.gameObject.transform.position;
        }
    }
}
