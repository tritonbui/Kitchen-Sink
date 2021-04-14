using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeTrap : MonoBehaviour
{
    public baseBlock _bb;
    public GameObject _deathField;
    public GameObject _spikes;

    public void Update()
    {
        deathToggle();
    }

    public void deathToggle()
    {
        if (_bb.isPowered && _deathField.activeSelf)
        {
            _deathField.SetActive(false);
            retractSpikes();
        }
        else if (!_bb.isPowered && !_deathField.activeSelf)
        {
            _deathField.SetActive(true);
            extendSpikes();
        }
    }

    public void extendSpikes()
    {
        _spikes.transform.position += new Vector3(0, 2.2f, 0);
    }

    public void retractSpikes()
    {
        _spikes.transform.position -= new Vector3(0, 2.2f, 0);
    }
}
