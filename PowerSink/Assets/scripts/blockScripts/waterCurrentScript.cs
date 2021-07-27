using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterCurrentScript : MonoBehaviour
{
    public Transform lerpDestination;
    public GameObject player;
    public float speed;

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (player == null)
            {
                player = col.gameObject;
            }

            //player.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (player != null)
            {
                player.GetComponent<Rigidbody>().useGravity = true;
            }

            player = null;
        }
    }

    public void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        if (player != null)
        {
            Vector3 targetDir = lerpDestination.position - player.transform.position;

            Vector3 targetVel = targetDir * speed;

            Rigidbody _rb = player.GetComponent<Rigidbody>();

            _rb.AddForce(targetVel - _rb.velocity, ForceMode.Impulse);
        }
    }
}
