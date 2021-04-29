using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeTrap : MonoBehaviour
{
    public Animator animator;
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
        animator.Play("spike_out", 0, 0f);
    }

    public void retractSpikes()
    {
        animator.Play("spike_in", 0, 0f);
    }
}
