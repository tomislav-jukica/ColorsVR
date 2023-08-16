using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Toggle _maleToggle;
    [SerializeField] private Toggle _femaleToggle;
    [SerializeField] private Toggle _otherToggle;

    private int _sexSelected = -1;

    public void OnMaleSelected(bool value)
    {
        if (value)
        {
            _sexSelected = 0;
            _femaleToggle.isOn = false;
            _otherToggle.isOn = false;
        }
    }

    public void OnFemaleSelected(bool value)
    {
        if (value)
        {
            _sexSelected = 1;
            _maleToggle.isOn = false;
            _otherToggle.isOn = false;
        }
    }

    public void OnOtherSelected(bool value)
    {
        if (value)
        {
            _sexSelected = 2;
            _maleToggle.isOn = false;
            _femaleToggle.isOn = false;
        }
    }
}
