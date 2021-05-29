using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class electricTrap : MonoBehaviour
{
    public baseBlock _bb;
    public AudioSource electricSound;
    public GameObject _deathField;

    public void Update()
    {
        deathToggle();
    }

    public void deathToggle() //toggles the death barrier around the electric trap and plays appropriate sounds
    {
        if (_bb.isPowered && !_deathField.activeSelf)
        {
            _deathField.SetActive(true);
            electricSound.Play();
        }
        else if (!_bb.isPowered && _deathField.activeSelf)
        {
            _deathField.SetActive(false);
            electricSound.Stop();
        }
    }
}
