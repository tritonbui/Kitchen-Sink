using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public bool isReturning;
    public float moveSpeed;
    private float startTime;
    private float journeyLength;
    
    public void Start()
    {
        startPoint.position = this.transform.position;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
    }

    public void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        
        if (this.transform.position == endPoint.position && !isReturning)
        {
            isReturning = true;
            startTime = Time.time;
            journeyLength = Vector3.Distance(endPoint.position, startPoint.position);
        }
        else if (this.transform.position == startPoint.position && isReturning)
        {
            isReturning = false;
            startTime = Time.time;
            journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
        }
        
        if (!isReturning)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fractionOfJourney);
        }
        else if (isReturning)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(endPoint.position, startPoint.position, fractionOfJourney);
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player") || col.gameObject.layer == LayerMask.NameToLayer("PowerOrb"))
        {
            col.transform.SetParent(this.transform);
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player") || col.gameObject.layer == LayerMask.NameToLayer("PowerOrb"))
        {
            col.transform.SetParent(null);
        }
    }
}
