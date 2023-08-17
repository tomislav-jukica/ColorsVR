using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ColorVoxel : MonoBehaviour
{
    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void SelectEnter(SelectEnterEventArgs args)
    {
        print("ye");
    }

    private void OnTriggerStay(Collider other)
    {   
        if (other.CompareTag("RightHand"))
        {
            _renderer.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RightHand"))
        {
            _renderer.enabled = true;
        }
    }
}
