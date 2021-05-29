using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbSpawnLogic : MonoBehaviour
{
    public playerInteraction _pi;
    public Transform target; //orb spawn point transform
    public float smoothSpeed = 0.125f;
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        transform.position = target.position; //sets spawn point collider position to orb spawn point position
    }

    private void OnTriggerEnter(Collider col)
    {
        if ((LayerMask.NameToLayer("Ground") == col.gameObject.layer || LayerMask.NameToLayer("PowerOrb") == col.gameObject.layer || LayerMask.NameToLayer("toggleSwitch") == col.gameObject.layer) && col.gameObject.tag != "receptacleBlock")
        {
            _pi.canPlaceOrb = false; //has detected the spawn point is inside of an object, and thus prevents the player from placing orb
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if ((LayerMask.NameToLayer("Ground") == col.gameObject.layer || LayerMask.NameToLayer("PowerOrb") == col.gameObject.layer || LayerMask.NameToLayer("toggleSwitch") == col.gameObject.layer) && col.gameObject.tag != "receptacleBlock")
        {
            _pi.canPlaceOrb = true; //has detected the spawn point isn't inside of an object, and thus allows the player to place the orb
        }
    }
}
