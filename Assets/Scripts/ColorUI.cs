using System;
using UnityEngine;
using System.IO;
using System.Net;
using TMPro;

public class ColorUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _colorToPickText;

    private ColorAPI.Color[] _colors;
    private ColorAPI.Color _colorToPick;

    private void Start()
    {
        GetColors();
        _colorToPick = GetColorToPick();
        _colorToPickText.text = _colorToPick.name;
    }

    private void GetColors()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://localhost:7052/api/color"));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        _colors = JsonHelper.GetJsonArray<ColorAPI.Color>(jsonResponse);
    }

    private ColorAPI.Color GetColorToPick()
    {
        //TODO
        return _colors[0];
    }
}

