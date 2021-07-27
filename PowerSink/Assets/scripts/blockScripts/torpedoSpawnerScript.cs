using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torpedoSpawnerScript : MonoBehaviour
{
    public GameObject torpedo;
    public baseBlock baseBlock;
    private GameObject spawnedTorpedo;
    public Transform target;

    public void Awake()
    {
        if (baseBlock.isPowered)
        {
            Spawn();
        }
    }

    public void FixedUpdate()
    {
        if (spawnedTorpedo == null && baseBlock.isPowered)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        spawnedTorpedo = Instantiate(torpedo, this.transform.position, Quaternion.identity);
        spawnedTorpedo.GetComponent<torpedoScript>().target = target;
    }
}
