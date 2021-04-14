using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class electricTrap : MonoBehaviour
{
    public baseBlock _bb;
    public GameObject _deathField;

    public void Update()
    {
        deathToggle();
    }

    public void deathToggle()
    {
        if (_bb.isPowered && !_deathField.activeSelf)
        {
            _deathField.SetActive(true);
        }
        else if (!_bb.isPowered && _deathField.activeSelf)
        {
            _deathField.SetActive(false);
        }
    }
}
