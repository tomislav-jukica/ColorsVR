using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageSlider : MonoBehaviour
{
    public Language Language;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private TextMeshProUGUI _description;

    public void OnSliderChanged(float value)
    {
        switch((int)value)
        {
            case 0:
                _description.text = "Ne pričam";
                break;
            case 1:
                _description.text = "Pasivno";
                break;
            case 2:
                _description.text = "Aktivno";
                break;
            case 3:
                _description.text = "Materinji";
                break;
        }

        _mainMenu.AddLanguage((int)Language, (int)value);
    }
}

public enum Language
{
    HRVATSKI,
    CAKAVSKI,
    KAJKAVSKI,
    ENGLESKI,
    ALBANSKI,
    BOŠNJAČKI,
    CRNOGORSKI,
    FRANCUSKI,
    MADARSKI,
    MAKEDONSKI,
    NJEMACKI,
    ROMSKI,
    RUSKI,
    SLOVENSKI,
    SRPSKI,
    TALIJANSKI,
    UKRAJINSKI,
    OSTALO
}
