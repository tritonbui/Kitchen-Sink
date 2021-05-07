using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueScript : MonoBehaviour
{
    public AudioSource dialogueSound = null;
    public bool hasPlayed = false;
    
    void OnTriggerEnter(Collider col)
    {
        if (LayerMask.NameToLayer("Player") == col.gameObject.layer && !hasPlayed)
        {
            dialogueSound?.Play();
            hasPlayed = true;
        }
    }
}
