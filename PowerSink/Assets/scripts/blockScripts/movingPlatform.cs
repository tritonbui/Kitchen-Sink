using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public bool isPowered;
    public bool isReturning;
    private bool canReturn;
    public float moveSpeed;
    public float bufferTime = 1f;
    private float startTime;
    private float journeyLength = 1f;
    public Animator animator;
    public bool isLightOn;
    
    public void Start()
    {
        startPoint.position = this.transform.position;
        isReturning = true;
    }

    public void Update()
    {
        Animations();
    }

    public void Animations()
    {
        if (isPowered && !isLightOn)
        {
            animator.Play("movingPlatOn", 0, 0f);
            isLightOn = true;
        }

        if (isPowered && isLightOn)
        {
            animator.Play("movingPlatOff", 0, 0f);
            isLightOn = false;
        }
    }

    public void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        
        if (this.transform.position == endPoint.position && !isReturning && isPowered)
        {
            isReturning = true;
            canReturn = false;
            Invoke("Buffer", bufferTime);
            journeyLength = Vector3.Distance(endPoint.position, startPoint.position);
        }
        else if (this.transform.position == startPoint.position && isReturning && isPowered)
        {
            isReturning = false;
            canReturn = false;
            Invoke("Buffer", bufferTime);
            journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
        }
        
        if (!isReturning && canReturn)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fractionOfJourney);
        }
        else if (isReturning && canReturn)
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

    public void Buffer()
    {
        startTime = Time.time;
        canReturn = true;
    }
}
