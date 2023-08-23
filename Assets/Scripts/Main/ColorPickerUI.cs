using System;
using UnityEngine;
using System.IO;
using System.Net;

public class ColorPickerUI : MonoBehaviour
{
    [SerializeField] private GridGenerator _gridGenerator;
    [SerializeField] private GameObject _colorButtonPrefab;
    [SerializeField] private GameObject _colorsParentObject;

    private ColorAPI.Color[] _colors;
    private ColorAPI.Color _selectedColor = null;

    private void Start()
    {
        GetColors();
        PopulateGrid();
    }

    public void PickColor(ColorAPI.Color color)
    {
        _selectedColor = color;
        _gridGenerator.TargetColor = color;
        _gridGenerator.GenerateGrid();
        transform.parent.gameObject.SetActive(false);
        
    }

    private void GetColors()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://localhost:7052/api/color"));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        _colors = JsonHelper.GetJsonArray<ColorAPI.Color>(jsonResponse);
    }

    private void PopulateGrid()
    {
        foreach (var color in _colors)
        {
            var buttonObject = Instantiate(_colorButtonPrefab, _colorsParentObject.transform);
            ColorButton colorButton = buttonObject.GetComponent<ColorButton>();
            colorButton.SetColor(color);
        }
    }
}

