using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ColorVoxel : MonoBehaviour
{
    private MeshRenderer _renderer;
    private GridGenerator _generator;

    public bool IsSelected = false;
    public Vector3 Position = Vector3.zero;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _generator = GetComponentInParent<GridGenerator>();
    }

    private void Update()
    {
        if (IsSelected && !_generator.RangesPicked && !_generator.IsEndStage)
        {
            //_renderer.enabled = false;
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
        else
        {
            //transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }
    }

    public void SelectEnter(SelectEnterEventArgs args)
    {
        _generator.OnVoxelSelected(this);
    }

    public void HoverEntered(HoverEnterEventArgs args)
    {
        //transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        _generator.AddVoxelInRange(this);
    }

    public void HoverExited(HoverExitEventArgs args)
    {
        //transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        _generator.RemoveVoxelInRange(this);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RightHand") && _generator.ClosestVoxel != this && !_generator.IsEndStage)
        {
            _renderer.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RightHand") && !_generator.IsEndStage)
        {
            _renderer.enabled = true;
        }
    }

    public void ToggleVisibility(bool isVisible, bool isEndStage = false)
    {
        if (!_generator.IsEndStage)
        {
            _renderer.enabled = isVisible;
        }

        if(isEndStage)
        {
            _renderer.enabled = isVisible;
        }
    }

    public string GetColor()
    {
        var color = ColorUtility.ToHtmlStringRGB(_renderer.material.color);
        return color;
    }
}
