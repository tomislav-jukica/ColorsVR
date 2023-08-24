using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool _onCooldown = false;

    public bool RangesPicked = false;

    public ColorAPI.Color TargetColor;

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

    public void OnVoxelSelected(ColorVoxel voxel)
    {
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
        }
        else if (_stage == 1 && !_onCooldown)
        {
            _closestVoxel.IsSelected = true;
            //_closestVoxel.gameObject.SetActive(false);
            _voxelRanges.Add(_closestVoxel);
            StartCoroutine(CooldownCoroutine());
            if (_voxelRanges.Count == 4)
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
                RangesPicked = true;
                _stage = 2;
                _colorConfirmUI.ShowUI(_stage);
            }
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        _onCooldown = false;
    }

    public void OnVoxelConfirmed()
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
        _mainMenu.SendToDatabase(_selectedVoxel, _voxelRanges);
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
        ToggleCubeVisibilty(true);
    }

    public void ToggleCubeVisibilty(bool isVisible, bool stage2 = false)
    {
        if (stage2)
        {
            for (int i = 0; i < _voxels.Count; i++)
            {
                if ((_voxels[i].Position.x == _selectedVoxel.Position.x && _voxels[i].Position.z == _selectedVoxel.Position.z) || 
                    (_voxels[i].Position.y == _selectedVoxel.Position.y && _voxels[i].Position.z == _selectedVoxel.Position.z))
                {
                    _voxels[i].gameObject.SetActive(isVisible);
                }
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
}