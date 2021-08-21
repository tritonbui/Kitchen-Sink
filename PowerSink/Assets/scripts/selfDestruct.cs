using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    public float deathTimer = 10f;

    public void Start()
    {
        Destroy(this.gameObject, deathTimer);
    }
}
