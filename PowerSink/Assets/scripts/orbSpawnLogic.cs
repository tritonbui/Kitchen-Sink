using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbSpawnLogic : MonoBehaviour
{
    public playerController playerController;
    public Transform target;
    public float smoothSpeed = 0.125f;
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        transform.position = target.position;
    }

    private void OnTriggerEnter(Collider col)
    {
        if ((LayerMask.NameToLayer("Ground") == col.gameObject.layer || LayerMask.NameToLayer("Default") == col.gameObject.layer || LayerMask.NameToLayer("PowerOrb") == col.gameObject.layer || LayerMask.NameToLayer("toggleSwitch") == col.gameObject.layer) && col.gameObject.tag != "receptacleBlock")
        {
            playerController.canPlaceOrb = false;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if ((LayerMask.NameToLayer("Ground") == col.gameObject.layer || LayerMask.NameToLayer("Default") == col.gameObject.layer || LayerMask.NameToLayer("PowerOrb") == col.gameObject.layer || LayerMask.NameToLayer("toggleSwitch") == col.gameObject.layer) && col.gameObject.tag != "receptacleBlock")
        {
            playerController.canPlaceOrb = true;
        }
    }
}
