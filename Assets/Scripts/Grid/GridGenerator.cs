using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VoxelPosition
{
    CORNER,
    SIDE,
    INSIDE
}

public class GridGenerator : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private int _gridX;
    [SerializeField] private int _gridY;
    [SerializeField] private int _gridZ;
    [SerializeField] private float _spaceBetween;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private ColorConfirmUI _colorConfirmUI;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private MainMenu _mainMenu;

    private List<ColorVoxel> _voxels = new();
    private ColorVoxel _selectedVoxel;
    private List<ColorVoxel> _voxelRanges = new();
    private int _stage = 0;

    private ColorVoxel _closestVoxel;
    private List<ColorVoxel> _voxelsInRange = new();

    public ColorVoxel ClosestVoxel { get => _closestVoxel; }
    public ColorVoxel SelectedVoxel { get => _selectedVoxel; }
    public List<ColorVoxel> VoxelRanges { get => _voxelRanges; }
    public bool IsEndStage { get => _stage == 3; }
    public int Stage { get => _stage; }

    private bool _onCooldown = false;
    private int _voxelsCollected = -1;

    public bool RangesPicked = false;

    public ColorAPI.Color TargetColor;

    private GridController _controller;
    private List<ColorVoxel> _totalVoxels = new();


    private void Awake()
    {
        _controller = GetComponentInParent<GridController>();
    }

    private void Start()
    {
        SetGridPosition();
    }

    private void SetGridPosition()
    {
        transform.localPosition = new Vector3(-_gridX * _spaceBetween / 2, -_gridY * _spaceBetween / 2, -_gridZ * _spaceBetween / 2);
    }

    public void GenerateGrid()
    {
        if (_voxels.Count == 0)
        {
            _mainMenu.CreateUser();
            for (int i = 0; i < _gridX; i++)
            {
                for (int j = 0; j < _gridY; j++)
                {
                    for (int k = 0; k < _gridZ; k++)
                    {
                        Vector3 position = new(transform.position.x + _spaceBetween * i, transform.position.y + _spaceBetween * j, transform.position.z + _spaceBetween * k);
                        var cube = Instantiate(_prefab, position, Quaternion.identity, transform);
                        var renderer = cube.GetComponent<MeshRenderer>();
                        renderer.material.color = new Color(1f - (1f / _gridX) * i, 1f - (1f / _gridY) * j, 1f - (1f / _gridZ) * k);
                        ColorVoxel colorVoxel = cube.GetComponent<ColorVoxel>();
                        colorVoxel.Position = new(i, j, k);
                        _voxels.Add(colorVoxel);
                    }
                }
            }
        }
        else
        {
            foreach (var voxel in _voxels)
            {
                voxel.ToggleVisibility(true);
                voxel.gameObject.SetActive(true);
            }
        }
        _controller.SetStartPosition();
    }

    public void OnVoxelSelected(ColorVoxel voxel)
    {
        if (_onCooldown) return;
        StartCoroutine(CooldownCoroutine());

        if (_stage == 0 && _selectedVoxel == null)
        {
            _selectedVoxel = _closestVoxel;
            for (int i = 0; i < _voxels.Count; i++)
            {
                if (_voxels[i] != _selectedVoxel.gameObject)
                {
                    _voxels[i].gameObject.SetActive(false);
                }
            }
            _selectedVoxel.ToggleVisibility(true);
            _colorConfirmUI.gameObject.SetActive(true);
            //_controller.SetStartPosition();
        }
        else if (_stage == 1)
        {
            _closestVoxel.IsSelected = true;
            //_closestVoxel.gameObject.SetActive(false);
            _voxelRanges.Add(_closestVoxel);
            if(_voxelRanges.Count == 6)
            {
                ShowPickedRanges();
                _stage = 2;
                _totalVoxels.AddRange(_voxelRanges);
                _colorConfirmUI.gameObject.SetActive(true);
                _colorConfirmUI.ShowUI(_stage);
                _controller.SetFinishPosition();
                //ToggleCubeVisibilty(false);
                _mainMenu.SendToDatabase(_selectedVoxel, _voxelRanges);
            }
            //switch (GetSelectedVoxelPosition())
            //{
            //    case VoxelPosition.CORNER:
            //        if (_voxelRanges.Count == 2)
            //        {
            //            ShowPickedRanges();
            //            _voxelsCollected = 2;
            //        }
            //        break;
            //    case VoxelPosition.SIDE:
            //        if (_voxelRanges.Count == 3)
            //        {
            //            ShowPickedRanges();
            //            _voxelsCollected = 3;
            //        }
            //        break;
            //    case VoxelPosition.INSIDE:
            //        if (_voxelRanges.Count == 4)
            //        {
            //            ShowPickedRanges();
            //            _voxelsCollected = 4;
            //        }
            //        break;
            //}
        }
        //else if (_stage == 2)
        //{
        //    _closestVoxel.IsSelected = true;
        //    //_closestVoxel.gameObject.SetActive(false);
        //    _voxelRanges.Add(_closestVoxel);

        //    if (_selectedVoxel.Position.z == 0 || _selectedVoxel.Position.z == 9)
        //    {
        //        if (_voxelRanges.Count == _voxelsCollected + 1)
        //        {
        //            _stage = 3;
        //            _totalVoxels.AddRange(_voxelRanges);
        //            _colorConfirmUI.gameObject.SetActive(true);
        //            _colorConfirmUI.ShowUI(_stage);
        //            _controller.SetFinishPosition();
        //            //ToggleCubeVisibilty(false);
        //            _mainMenu.SendToDatabase(_selectedVoxel, _voxelRanges);
        //        }
        //    }
        //    else
        //    {
        //        if (_voxelRanges.Count == _voxelsCollected + 2)
        //        {
        //            _stage = 3;
        //            _totalVoxels.AddRange(_voxelRanges);
        //            _colorConfirmUI.gameObject.SetActive(true);
        //            _colorConfirmUI.ShowUI(_stage);
        //            _controller.SetFinishPosition();
        //            //ToggleCubeVisibilty(false);
        //            _mainMenu.SendToDatabase(_selectedVoxel, _voxelRanges);

        //        }
        //    }
        //}
    }


    private VoxelPosition GetSelectedVoxelPosition()//TODO don't hardcode
    {
        bool xPos = false;
        bool yPos = false;

        if (_selectedVoxel.Position.x == 0 || _selectedVoxel.Position.x == 9)
        {
            xPos = true;
        }

        if (_selectedVoxel.Position.y == 0 || _selectedVoxel.Position.y == 9)
        {
            yPos = true;
        }

        if (xPos && yPos) return VoxelPosition.CORNER;
        else if (xPos || yPos) return VoxelPosition.SIDE;
        else return VoxelPosition.INSIDE;
    }

    private void ShowPickedRanges()
    {
        _colorConfirmUI.gameObject.SetActive(true);
        for (int i = 0; i < _voxels.Count; i++)
        {
            var success = _voxelRanges.Find(x => x.gameObject == _voxels[i]);
            if (success == null)
            {
                _voxels[i].gameObject.SetActive(false);
            }
            else
            {
                success.ToggleVisibility(true);
            }
        }
        _stage = 2;
        _colorConfirmUI.ShowUI(_stage);
        _controller.SetStartPosition();
    }

    private IEnumerator CooldownCoroutine()
    {
        _onCooldown = true;
        yield return new WaitForSeconds(0.1f);
        _onCooldown = false;
    }

    public void OnVoxelConfirmed() //confirmed 1 voxel
    {
        _stage = 1;
        _colorConfirmUI.ShowUI(_stage);
    }

    public void OnVoxelCanceled()
    {
        _selectedVoxel = null;
        ToggleCubeVisibilty(true);
        FindClosestVoxel();
        _colorConfirmUI.gameObject.SetActive(false);
    }

    public void ConfirmRange()
    {
        if (_stage == 2)
        {
            _colorConfirmUI.gameObject.SetActive(false);
            //ShowDepth();
        }
    }

    private void ShowDepth()
    {
        for (int i = 0; i < _voxels.Count; i++)
        {
            if (_voxels[i].Position.x == _selectedVoxel.Position.x && _voxels[i].Position.y == _selectedVoxel.Position.y)
            {
                _voxels[i].gameObject.SetActive(true);
            }
        }
    }

    public void CancelRange()
    {
        foreach (var voxel in _voxelRanges)
        {
            voxel.IsSelected = false;
        }
        _stage = 1;
        _voxelRanges.Clear();
        RangesPicked = false;
        ToggleCubeVisibilty(true, true);
    }

    public void ToggleCubeVisibilty(bool isVisible, bool stage2 = false)
    {
        if (stage2)
        {
            //for (int i = 0; i < _voxels.Count; i++)
            //{
            //    if ((_voxels[i].Position.x == _selectedVoxel.Position.x && _voxels[i].Position.z == _selectedVoxel.Position.z) ||
            //        (_voxels[i].Position.y == _selectedVoxel.Position.y && _voxels[i].Position.z == _selectedVoxel.Position.z))
            //    {
            //        _voxels[i].gameObject.SetActive(isVisible);
            //    }
            //}
            for (int i = 0; i < _voxels.Count; i++)
            {
                _voxels[i].gameObject.SetActive(isVisible);
            }
        }
        else
        {
            for (int i = 0; i < _voxels.Count; i++)
            {
                _voxels[i].gameObject.SetActive(isVisible);
            }
        }
    }

    public void AddVoxelInRange(ColorVoxel voxel)
    {
        if (!_voxelsInRange.Contains(voxel))
        {
            _voxelsInRange.Add(voxel);
        }
        FindClosestVoxel();
    }

    public void RemoveVoxelInRange(ColorVoxel voxel)
    {
        if (_voxelsInRange.Contains(voxel))
        {
            _voxelsInRange.Remove(voxel);
        }
        FindClosestVoxel();
    }

    private void FindClosestVoxel()
    {
        float minDistance = Mathf.Infinity;
        for (int i = 0; i < _voxelsInRange.Count; i++)
        {
            var voxel = _voxelsInRange[i];
            var distance = Vector3.Distance(voxel.transform.position, _rightHand.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                _closestVoxel = voxel;
            }
            voxel.transform.localScale = new(0.05f, 0.05f, 0.05f);
        }
        _closestVoxel.transform.localScale = new(0.075f, 0.075f, 0.075f);
        _closestVoxel.ToggleVisibility(true);
    }

    internal void Restart()
    {
        _voxels = new();
        _selectedVoxel = null;
        _voxelRanges = new();
        _stage = 0;
        _closestVoxel = null;
        _voxelsInRange = new();
        _onCooldown = false;
        _voxelsCollected = -1;
        RangesPicked = false;
        TargetColor = null;
        _totalVoxels = new();

        _colorConfirmUI.gameObject.SetActive(false);
        _mainMenu.Restart();
    }

    internal void DoAnotherOne()
    {
        //_voxels = new();
        _selectedVoxel = null;
        _voxelRanges = new();
        _stage = 0;
        _closestVoxel = null;
        _voxelsInRange = new();
        _onCooldown = false;
        _voxelsCollected = -1;
        RangesPicked = false;
        TargetColor = null;

        _colorConfirmUI.gameObject.SetActive(false);
        _colorConfirmUI.DoAnotherOne();
        //_controller.ResetRotation();
        _mainMenu.DoAnotherOne();
    }

    internal void ShowAllRanges()
    {
        ToggleCubeVisibilty(true);

        foreach (var voxel in _voxels)
        {
            voxel.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            voxel.IsSelected = false;
            if (_totalVoxels.Contains(voxel))
            {
                voxel.ToggleVisibility(true, true);
            }
            else
            {
                voxel.ToggleVisibility(false, true);
            }
        }
    }
}