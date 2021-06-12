using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportScript : MonoBehaviour
{
    public Transform targetLocation;
    public teleportScript otherEnd;
    public bool isReadyPlayer;
    public bool isReadyOrb;

    public void Start()
    {
        isReadyPlayer = true;
        isReadyOrb = true;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (LayerMask.NameToLayer("Player") == col.gameObject.layer)
        {
            Debug.Break();
            if (isReadyPlayer)
            {
                otherEnd.isReadyPlayer = false;
                col.gameObject.GetComponent<Rigidbody>().position = targetLocation.position;
                col.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Debug.Break();
            }
        }

        if (LayerMask.NameToLayer("PowerOrb") == col.gameObject.layer)
        {
            if (isReadyOrb)
            {
                otherEnd.isReadyOrb = false;
                col.gameObject.GetComponent<Rigidbody>().position = targetLocation.position;
            }
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (LayerMask.NameToLayer("Player") == col.gameObject.layer)
        {
            if (!isReadyPlayer)
            {
                isReadyPlayer = true;
            }
        }

        if (LayerMask.NameToLayer("PowerOrb") == col.gameObject.layer)
        {
            if (!isReadyOrb)
            {
                isReadyOrb = true;
            }
        }
    }
}
