using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torpedoSpawnerScript : MonoBehaviour
{
    public GameObject torpedo;
    public baseBlock baseBlock;
    private GameObject spawnedTorpedo;
    public Transform target;
    public bool isLightOn;
    public Animator animator;

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

    public void Update()
    {
        Animations();
    }

    public void Animations()
    {
        if (baseBlock.isPowered && !isLightOn)
        {
            animator.Play("torpedoSpawnerOn", 0, 0f);
            isLightOn = true;
        }

        if (!baseBlock.isPowered && isLightOn)
        {
            animator.Play("torpedoSpawnerOff", 0, 0f);
            isLightOn = false;
        }
    }
}
