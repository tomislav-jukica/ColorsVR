using UnityEngine;

public class RotationLocker : MonoBehaviour
{
    void Update()
    {
        transform.eulerAngles = new Vector3(0,0,0);
    }
}
