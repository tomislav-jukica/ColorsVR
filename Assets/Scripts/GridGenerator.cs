using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private int _gridX;
    [SerializeField] private int _gridY;
    [SerializeField] private int _gridZ;
    [SerializeField] private float _spaceBetween;
    [SerializeField] private GameObject _prefab;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
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
                    print(renderer.material.color);
                }
            }
        }
    }
}
