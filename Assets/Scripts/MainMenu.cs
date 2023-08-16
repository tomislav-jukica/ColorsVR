using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GridGenerator _gridGenerator;

    [Header("Objects")]
    [SerializeField] private Toggle _maleToggle;
    [SerializeField] private Toggle _femaleToggle;
    [SerializeField] private Toggle _otherToggle;
    [SerializeField] private TextMeshProUGUI _ageText;
    [SerializeField] private GameObject _educationPicker;
    [SerializeField] private TMP_Dropdown _educationDropdown;

    private int _sexSelected = -1;
    private int _age = 0;
    private int _workStatus = 0;
    private int _education = -1;

    #region Sex
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
    #endregion

    #region Age
    public void OnPickAge(System.Single age)
    {
        _ageText.text = age.ToString();
        _age = (int)age;
    }
    #endregion

    public void OnWorkStatusChanged(int value)
    {
        _workStatus = value; //0-ucenik, 1-student, 2-zaposlen, 3-nezaposlen
        if (value == 2 || value == 3)
        {
            _educationPicker.SetActive(true);
            _education = 0;
        }
        else
        {
            _educationPicker.SetActive(false);
            _educationDropdown.SetValueWithoutNotify(0);
            _education = -1;
        }
    }

    public void OnEducationChanged(int value)
    {
        _education = value;
    }

    public void OnConfirm()
    {
        if (Validate())
        {
            _gridGenerator.GenerateGrid();
            gameObject.SetActive(false);
        }
    }

    private bool Validate()
    {
        if (!_maleToggle.isOn && !_femaleToggle.isOn && !_otherToggle.isOn) return false;
        if (_age <= 0) return false;

        return true;
    }
}
