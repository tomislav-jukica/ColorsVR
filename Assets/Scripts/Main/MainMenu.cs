using ColorAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GridGenerator _gridGenerator;
    private DatabaseManager _databaseManager;

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
    [SerializeField] private GameObject _page0;
    [SerializeField] private GameObject _page1;
    [SerializeField] private GameObject _page2;
    [SerializeField] private GameObject _page3;
    [SerializeField] private GameObject _page4;
    [SerializeField] private GameObject _page5;
    [SerializeField] private GameObject _page6;
    [SerializeField] private GameObject _specialName;
    [SerializeField] private GameObject _howToUse;
    [SerializeField] private GameObject _aboutUs;

    private int _sexSelected = -1;
    private int _age = 0;
    private int _workStatus = 0;
    private int _education = 0;
    private int _work = 0;
    private Dictionary<int, int> _languages = new();
    private int _health = 0;
    private int _workScreenTime = 0;
    private int _soloScreenTime = 0;
    private int _nature = 0;
    private int _location = 0;
    private int _population = 0;

    private int _page = 0;

    private User _user;

    private void Awake()
    {
        _databaseManager = GetComponent<DatabaseManager>();
    }

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
        switch ((int)value)
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

    #region Page 5
    public void OnLocationChanged(int value)
    {
        _location = value;
    }

    public void OnPopulationChanged(int value)
    {
        _population = value;
    }
    #endregion

    public void OnConfirm()
    {
        if (_specialName.activeSelf || _aboutUs.activeSelf || _howToUse.activeSelf)
        {
            _specialName.SetActive(false);
            _aboutUs.SetActive(false);
            _howToUse.SetActive(false);
            _page6.SetActive(true);
            return;
        }

        if (!Validate()) return;

        if (_page == 0)
        {
            _page0.SetActive(false);
            _page1.SetActive(true);
            _page++;
        }
        else if (_page == 1)
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
            _page4.SetActive(false);
            _page5.SetActive(true);
            _page++;
        }
        else if (_page == 5)
        {
            _page5.SetActive(false);
            _page6.SetActive(true);
            _page++;
        }
        //else if (_page == 6)
        //{
        //    CreateUser();
        //    _gridGenerator.GenerateGrid();
        //    gameObject.SetActive(false);
        //}
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

    public void SendToDatabase(ColorVoxel selectedVoxel, List<ColorVoxel> voxelRanges)
    {
        List<ColorAPI.Language> languages = new List<ColorAPI.Language>();
        foreach (KeyValuePair<int, int> language in _languages)
        {
            ColorAPI.Language newLanguage = new ColorAPI.Language(language.Key, language.Value, _user.Id);
            languages.Add(newLanguage);
        }

        PickedColor pickedColor = new PickedColor(_gridGenerator.TargetColor.value, selectedVoxel.GetColor(), _user.Id);

        List<ColorRange> colorRanges = new List<ColorRange>();
        foreach (var voxel in voxelRanges)
        {
            ColorRange colorRange = new ColorRange(voxel.GetColor(), pickedColor.Id);
            colorRanges.Add(colorRange);
        }

        _databaseManager.SendData(_user, languages, pickedColor, colorRanges);
    }

    public void CreateUser()
    {
        _user = new User(_sexSelected, _age, _workStatus, _education, _work, _health, _workScreenTime, _soloScreenTime, _nature, _location, _population);
    }

    public void ShowAboutUs()
    {
        _aboutUs.SetActive(true);
        _page6.SetActive(false);
    }

    public void ShowHowToUse()
    {
        _howToUse.SetActive(true);
        _page6.SetActive(false);
    }

    public void ShowSpecialName()
    {
        _specialName.SetActive(true);
        _page6.SetActive(false);
    }

    public void Restart()
    {
        _sexSelected = -1;
        _age = 0;
        _workStatus = 0;
        _education = 0;
        _work = 0;
        _languages = new();
        _health = 0;
        _workScreenTime = 0;
        _soloScreenTime = 0;
        _nature = 0;
        _location = 0;
        _population = 0;
        _page = 0;
        _user = null;

        gameObject.SetActive(true);
        _page0.SetActive(true);
    }
}
