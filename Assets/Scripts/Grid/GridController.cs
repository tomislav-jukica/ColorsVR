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
    private void Update()
    {
        if (_rotating && CanMove)
        {
            if (_isHandStartSet)
            {
                offset = new(0, _handStart.y - rightHand.transform.eulerAngles.y, 0);

                _handRotation = new Vector3(0, offset.y, 0); //150 | 0 | 10

                if (_handRotation != Vector3.zero)
                {
                    print(_handRotation.y);
                    transform.eulerAngles = new Vector3(0, _lastRotation.y + _handRotation.y, 0); //150 + 0 | skiped | 150 + 10         
                }
            }
            else
            {
                _isHandStartSet = true;
                _handStart = new(0, rightHand.transform.eulerAngles.y, 0);
            }

            

        }
        else
        {
            _lastRotation = transform.eulerAngles;

            _isHandStartSet = false;
            _handStart = Vector3.zero;
        }




        //if (!_isLastRotationSet)
        //{
        //    offset = rightHand.transform.eulerAngles;
        //    _isLastRotationSet = true;
        //}

        //if (_currentRotation != rightHand.transform.eulerAngles)
        //{
        //    transform.eulerAngles -= rightHand.transform.eulerAngles - offset;
        //    _currentRotation = rightHand.transform.eulerAngles;
        //}

        //offset = rightHand.transform.eulerAngles;


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
