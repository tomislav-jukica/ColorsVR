using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerDuo : MonoBehaviour
{
    [SerializeField] private Color _defaultColor = Color.white;

    private Material _material;
    private float _distanceModifier;
    private bool _isLocked = false;

    private Collider _collider;
    private MeshRenderer _renderer;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _material = _renderer.material;
        _material.color = _defaultColor;
        _distanceModifier = 1 / transform.localScale.x;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_isLocked && other.CompareTag("RightHand"))
        {
            Vector3 difference = (_collider.transform.position - other.transform.position) * 6;
            difference = new Vector3(Mathf.Abs(difference.x), Mathf.Abs(difference.y), Mathf.Abs(difference.z));
            print(difference);
            _material.color = Color.HSVToRGB(difference.x, difference.y, difference.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_isLocked && other.CompareTag("RightHand"))
        {
            _material.color = _defaultColor;
        }
    }

    public void ToggleLockColor()
    {
        if (_material.color != _defaultColor)
        {
            _isLocked = !_isLocked;
            if (!_isLocked)
            {
                _material.color = _defaultColor;
            }
        }
    }
}
