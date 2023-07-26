using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private ColorChangerDuo _colorChangerDuo;
    [SerializeField] private ColorChangerDuo _colorChangerDuoSphere;

    //public void OnRightControllerPrimaryButton()
    //{
    //    _colorChanger.ToggleLockColor();
    //    _colorChangerDuo.ToggleLockColor();
    //    _colorChangerDuoSphere.ToggleLockColor();
    //}

    //public void OnColorPicked()
    //{
    //    //TODO picked color
    //}
}
