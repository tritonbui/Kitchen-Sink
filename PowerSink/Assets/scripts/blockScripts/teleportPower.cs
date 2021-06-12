using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportPower : MonoBehaviour
{
    public baseBlock teleporterA;
    public GameObject tATrigger;
    public baseBlock teleporterB;
    public GameObject tBTrigger; //teleporter trigger

    public void FixedUpdate()
    {
        TriggerPower(teleporterA, tATrigger);
        TriggerPower(teleporterB, tBTrigger);
    }

    public void TriggerPower(baseBlock teleporter, GameObject trigger)
    {
        if (teleporter.isPowered)
        {
            trigger.SetActive(true);
        }
        else
        {
            trigger.SetActive(false);
        }
    }
}
