using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    [SerializeField] private List<Color> _colors = new List<Color>();
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _scaleReductionValue = 2f;
    [SerializeField] private float _explosionForceMultiplier = 2f;
    [SerializeField] private float _explosionRadiusMultiplier = 2f;

    public Cube Generate(Vector3 scale, float force, float radius, float separationChance)
    {
        Cube cube = Instantiate(_prefab);

        if (cube != null) 
        {
            cube.gameObject.transform.localScale = scale / _scaleReductionValue;
            cube.Initialize(force * _explosionForceMultiplier, radius * _explosionRadiusMultiplier, separationChance);

            if (cube.TryGetComponent(out MeshRenderer meshRenderer))
                meshRenderer.material.color = _colors[Random.Range(0, _colors.Count - 1)];
        }

        return cube;
    }
}
