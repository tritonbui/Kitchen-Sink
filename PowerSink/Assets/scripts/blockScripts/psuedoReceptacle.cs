using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psuedoReceptacle : MonoBehaviour
{
    public receptacleBlock _rcpB;
    public GameObject _pop;
    public GameObject _spawnedPop;
    public Transform _orbSpawn;
    public bool doesStartWithOrb = false;

    public void Start()
    {
        if (doesStartWithOrb)
        {
            startWithOrb();
        }
    }

    public void startWithOrb() //sets up a power orb to start in the psuedo receptacle
    {
        GameObject newOrb = Instantiate(_pop, _orbSpawn.position, Quaternion.identity);
        _spawnedPop = newOrb;
        _spawnedPop.transform.GetChild(1).gameObject.GetComponent<powerOrb>().psuedoReceptacle = this.gameObject;
        _rcpB.insertedPowerOrb = newOrb.transform.GetChild(1).gameObject;
        _rcpB.insertedPowerOrb.SetActive(false);
        _rcpB.hasPowerOrb = true;
    }

    public void Respawn() //respawns powerOrb that started in the current psuedo receptacle
    {
        _spawnedPop.transform.GetChild(1).gameObject.transform.position = _orbSpawn.position;
        _rcpB.insertedPowerOrb = _spawnedPop.transform.GetChild(1).gameObject;
        _rcpB.insertedPowerOrb.SetActive(false);
        _rcpB.hasPowerOrb = true;
    }
}
