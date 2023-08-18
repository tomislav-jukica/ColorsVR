using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorConfirmUI : MonoBehaviour
{
    [SerializeField] private GridGenerator _gridGenerator;
    [SerializeField] private GridController _gridController;
    [SerializeField] private GameObject _stage0;
    [SerializeField] private GameObject _stage1;
    [SerializeField] private GameObject _stage2;

    private void OnEnable()
    {
        _gridController.CanMove = false;
    }

    private void OnDisable()
    {
        _gridController.CanMove = true;
    }

    public void OnConfirm()
    {
        _gridGenerator.OnVoxelConfirmed();
    }

    public void OnCancel()
    {
        _gridGenerator.OnVoxelCanceled();
    }

    public void OnSecondStageStart()
    {
        _stage1.SetActive(false);
        gameObject.SetActive(false);
        _gridGenerator.ToggleCubeVisibilty(true);
    }

    public void OnConfirmRange()
    {

    }

    public void OnCancelRange()
    {

    }

    public void ShowUI(int stage)
    {
        if (stage == 0)
        {
            _stage0.SetActive(true);
            _stage1.SetActive(false);
            _stage2.SetActive(false);
        }
        else if (stage == 1)
        {
            _stage0.SetActive(false);
            _stage1.SetActive(true);
            _stage2.SetActive(false);
        }
        else if (stage == 2)
        {
            _stage0.SetActive(false);
            _stage1.SetActive(false);
            _stage2.SetActive(true);
        }
    }
}
