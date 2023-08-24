using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorConfirmUI : MonoBehaviour
{
    [SerializeField] private GridGenerator _gridGenerator;
    [SerializeField] private GridController _gridController;
    [SerializeField] private GameObject _stage0;
    [SerializeField] private GameObject _stage1;
    [SerializeField] private GameObject _stage2;
    [SerializeField] private GameObject _stage3;

    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private TextMeshProUGUI _stage2Text;

    private void OnEnable()
    {
        _gridController.CanMove = false;
        _buttonText.text = "Da, to je " + _gridGenerator.TargetColor.name;
        _stage2Text.text = $"Molimo odaberite raspon boja koje pokriva tako da odaberete najudaljenije nijanse koje se još uvijek mogu nazvati {_gridGenerator.TargetColor.name}. " +
            $"Odaberi 4 najudaljenije nijanse u sva 4 smjera – lijevo, desno, gore i dolje.";
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
        _gridGenerator.ToggleCubeVisibilty(true, true);
    }

    public void OnConfirmRange()
    {
        _gridGenerator.ConfirmRange();
    }

    public void OnCancelRange()
    {
        _gridGenerator.CancelRange();
    }

    public void OnFinishSession()
    {

    }

    public void OnDoAnthorOne()
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
        } else if (stage == 3)
        {
            _stage0.SetActive(false);
            _stage1.SetActive(false);
            _stage2.SetActive(false);
            _stage3.SetActive(true);
        }
    }
}
