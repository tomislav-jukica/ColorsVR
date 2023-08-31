using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GridController : MonoBehaviour
{
    public ActionBasedController rightHand;
    [SerializeField] private float _maxSpeed;
    public bool CanMove = true;
    public Vector3 StartPosition;
    public Vector3 FinishPosition;

    private Vector3 _handRotation = Vector3.zero;
    private Vector3 _lastRotation = Vector3.zero;
    private bool _isHandStartSet = false;
    private bool _rotating = false;

    private Vector3 _handStart = Vector3.zero;

    Vector3 offset;
    float x, y, z = 0f;
    private void Update()
    {
        if (_rotating && CanMove)
        {
            if (_isHandStartSet)
            {
                offset = new(_handStart.x - rightHand.transform.eulerAngles.x, _handStart.y - rightHand.transform.eulerAngles.y, 0);

                _handRotation = new Vector3(offset.x, offset.y, offset.z);

                if (_handRotation != Vector3.zero)
                {
                    x = _lastRotation.x - _handRotation.x;
                    if (x > 360)
                    {
                        x -= 360;
                    }
                    else if (x < 0)
                    {
                        x += 360;
                    }
                    y = _lastRotation.y + _handRotation.y;

                    transform.rotation = Quaternion.Euler(x, y, z);

                }
            }
            else
            {
                _isHandStartSet = true;
                _handStart = new(rightHand.transform.eulerAngles.x, rightHand.transform.eulerAngles.y, rightHand.transform.eulerAngles.z);
            }



        }
        else
        {
            _lastRotation = new(x,y,z);

            _isHandStartSet = false;
            _handStart = Vector3.zero;
        }
    }



    public void GripPressed()
    {
        _rotating = true;
    }

    public void GripRelesed()
    {
        _isHandStartSet = false;
        _rotating = false;
    }

    public void ResetRotation()
    {
        transform.eulerAngles = Vector3.zero;
        _handRotation = Vector3.zero;
    }

    public void SetFinishPosition()
    {
        transform.position = FinishPosition;
        ResetRotation();
    }

    public void SetStartPosition()
    {
        transform.position = StartPosition;
        ResetRotation();
    }
}
