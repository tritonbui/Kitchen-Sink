using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate() //smoothly transitions the player follow point (which the Cinemachine camera is locked to) from it's current position to the player's current position
    {
        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
