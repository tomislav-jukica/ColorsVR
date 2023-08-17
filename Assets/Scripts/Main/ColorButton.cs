using TMPro;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
    private ColorAPI.Color _color;

    private TextMeshProUGUI _buttonText;
    private ColorPickerUI _pickerManager;

    private void Awake()
    {
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        _pickerManager = transform.parent.GetComponentInParent<ColorPickerUI>();
    }

    public void SetColor(ColorAPI.Color color)
    {
        _color = color;
        _buttonText.text = _color.name;
    }

    public void OnClick()
    {
        _pickerManager.PickColor(_color);
    }
}
