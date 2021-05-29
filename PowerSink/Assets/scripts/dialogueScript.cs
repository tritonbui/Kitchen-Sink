using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueScript : MonoBehaviour
{
    public AudioSource dialogueSound = null; // audio source component
    public bool hasPlayed = false; //bool for checking audio hasn't already been played
    
    void OnTriggerEnter(Collider col)
    {
        //plays dialogue when player enters collider & only plays it once
        if (LayerMask.NameToLayer("Player") == col.gameObject.layer && !hasPlayed)
        {
            dialogueSound?.Play();
            hasPlayed = true;
        }
    }
}
