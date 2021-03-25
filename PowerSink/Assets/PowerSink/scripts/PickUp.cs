using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform theDest;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //when user presses E key, it calls function to pick up object
        {
            OnObjectPickUp();
        }

        if (Input.GetKeyUp(KeyCode.E)) //releases object when E key is released
        {
            OnObjectRelease();
        }

    }


    void OnObjectPickUp() //pickup function
    {
        GetComponent<SphereCollider>().enabled = false; //object does not collide with other objects when picked up
        GetComponent<Rigidbody>().useGravity = false; //when object is picked up, it won't fall as gravity is disbaled during pickup
        this.transform.position = theDest.position; //transform position of object will be in front of player
        this.transform.parent = GameObject.Find("Destination").transform; //parents object to transform position where objects are supposed to be when picked up
    }

    void OnObjectRelease()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<SphereCollider>().enabled = true;
    }
}
