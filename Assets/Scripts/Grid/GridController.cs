using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GridController : MonoBehaviour
{
    public ActionBasedController rightHand;
    [SerializeField] private float _maxSpeed;

    private Vector3 _currentRotation;
    private Vector3 _lastRotation;
    private bool _isLastRotationSet = false;
    private bool _rotating = false;

    Vector3 offset;
    private void Update()
    {
        if(_rotating)
        {
            if (!_isLastRotationSet)
            {
                offset = rightHand.transform.eulerAngles;
                _isLastRotationSet = true;
            }

            if (_currentRotation != rightHand.transform.eulerAngles)
            {
                transform.eulerAngles -= Vector3.ClampMagnitude((rightHand.transform.eulerAngles - offset), _maxSpeed);
                //_currentRotation = rightHand.transform.eulerAngles;
            }
            offset = rightHand.transform.eulerAngles;
        }
    }

    
    public void GripPressed()
    {
        _rotating = true;        
    }

    public void GripRelesed()
    {
        _isLastRotationSet = false;
        _rotating = false;
    }
}
