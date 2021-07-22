using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterCurrentScript : MonoBehaviour
{
    public Transform lerpDestination;
    public GameObject player;
    public Rigidbody _rb;
    public float speed;

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (player == null)
            {
                player = col.gameObject;
            }

            player.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.GetComponent<Rigidbody>().useGravity = true;
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
            player.transform.position = Vector3.Lerp(player.transform.position, lerpDestination.position, Time.deltaTime * speed);
        }
    }
}
