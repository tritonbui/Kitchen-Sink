using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerCoreScript : MonoBehaviour
{
    public baseBlock _bb;

    public void FixedUpdate()
    {
        emmitPower();
    }

    public void emmitPower()
    {
        if (_bb._top != null)
        {
            powerCheck(_bb._top);
        }

        if (_bb._bot != null)
        {
            powerCheck(_bb._bot);
        }

        if (_bb._front != null)
        {
            powerCheck(_bb._front);
        }

        if (_bb._back != null)
        {
            powerCheck(_bb._back);
        }

        if (_bb._left != null)
        {
            powerCheck(_bb._left);
        }

        if (_bb._right != null)
        {
            powerCheck(_bb._right);
        }

        if (!_bb.isPowered)
        {
            _bb.isPowered = true;
        }
    }

    public void powerCheck(GameObject obj)
    {
        if (!obj.GetComponent<baseBlock>().isPowered)
        {
            obj.GetComponent<baseBlock>().receptaclePowerOn();
        }
    }
}
