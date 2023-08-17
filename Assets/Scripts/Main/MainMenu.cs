using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GridGenerator _gridGenerator;

    [Header("Page 1")]
    [SerializeField] private Toggle _maleToggle;
    [SerializeField] private Toggle _femaleToggle;
    [SerializeField] private Toggle _otherToggle;
    [SerializeField] private TextMeshProUGUI _ageText;
    [SerializeField] private GameObject _educationPicker;
    [SerializeField] private TMP_Dropdown _educationDropdown;
    [Header("Page 2")]
    [SerializeField] private TextMeshProUGUI _description;
    [Header("Page 4")]
    [SerializeField] private TextMeshProUGUI _workScreenDescription;
    [SerializeField] private TextMeshProUGUI _soloScreenDescription;
    [Header("Pages")]
    [SerializeField] private GameObject _page1;
    [SerializeField] private GameObject _page2;
    [SerializeField] private GameObject _page3;
    [SerializeField] private GameObject _page4;

    private int _sexSelected = -1;
    private int _age = 0;
    private int _workStatus = 0;
    private int _education = -1;
    private int _work = 0;
    private Dictionary<int, int> _languages;
    private int _health = 0;
    private int _workScreenTime = 0;
    private int _soloScreenTime = 0;
    private int _nature = 0;



    private int _page = 1;

    #region Page 1
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

    public void OnPickAge(System.Single age)
    {
        _ageText.text = age.ToString();
        _age = (int)age;
    }


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
    #endregion

    #region Page 2
    public void OnChangeWork(int value)
    {
        _work = value;
        switch (_work)
        {
            case 0:
                _description.text = "(matematika, kemija, fizika,\r\nbiologija, geologija, ekologija)";
                break;
            case 1:
                _description.text = "(strojarstvo, sve inženjerske struke, računalne struke)";
                break;
            case 2:
                _description.text = "(sve medicinske i farmaceutske struke)";
                break;
            case 3:
                _description.text = "(sve poljoprivredne i šumarske\r\nstruke)";
                break;
            case 4:
                _description.text = "(sve ekonomske, političke, komunikacijske i pravne struke, kineziologija,\r\nsociologija, pedagogija i psihologija)";
                break;
            case 5:
                _description.text = "(sve filozofske, teološke, jezične i povijesne\r\ndiscipline)";
                break;
            case 6:
                _description.text = "(arhitektura, dizajn, film i sve likovno-umjetničke struke)";
                break;
            case 7:
                _description.text = "(dramske, glazbene, plesne i književne struke)";
                break;
        }
    }
    #endregion

    #region Page 3
    public void AddLanguage(int language, int value)
    {
        if (_languages.ContainsKey(language))
        {
            _languages[language] = value;
        }
        else
        {
            _languages.Add(language, value);
        }
    }
    #endregion

    #region Page 4
    public void OnHealthChanged(int value)
    {
        _health = value;
    }
    public void OnWorkScreenTimeChanged(float value)
    {
        _workScreenTime = (int)value;
        switch((int)value)
        {
            case 0:
                _workScreenDescription.text = "Nikad";
                break;
            case 1:
                _workScreenDescription.text = "Povremeno";
                break;
            case 2:
                _workScreenDescription.text = "Stalno";
                break;
        }
    }
    public void OnSoloScreenTimeChanged(float value)
    {
        _soloScreenTime = (int)value;
        switch ((int)value)
        {
            case 0:
                _soloScreenDescription.text = "Manje od 1h";
                break;
            case 1:
                _soloScreenDescription.text = "Manje od 3h";
                break;
            case 2:
                _soloScreenDescription.text = "Više od 3h";
                break;
        }
    }
    public void OnNatureChanged(int value)
    {
        _nature = value;
    }
    #endregion
    public void OnConfirm()
    {
        if (!Validate()) return;

        if (_page == 1)
        {
            _page1.SetActive(false);
            _page2.SetActive(true);
            _page++;
        }
        else if (_page == 2)
        {
            _page2.SetActive(false);
            _page3.SetActive(true);
            _page++;
        }
        else if (_page == 3)
        {
            _page3.SetActive(false);
            _page4.SetActive(true);
            _page++;
        }
        else if (_page == 4)
        {
            _gridGenerator.GenerateGrid();
            gameObject.SetActive(false);
        }
    }

    private bool Validate()
    {
        if (_page == 1)
        {
            if (!_maleToggle.isOn && !_femaleToggle.isOn && !_otherToggle.isOn) return false;
            if (_age <= 0) return false;
        }

        return true;
    }
}
