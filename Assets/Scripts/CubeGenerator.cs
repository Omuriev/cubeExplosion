using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    [SerializeField] private List<Color> _colors = new List<Color>();
    [SerializeField] private Cube _prefab;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private float _scaleReductionValue = 2f;
    [SerializeField] private float _explosionForceMultiplier = 2f;
    [SerializeField] private float _explosionRadiusMultiplier = 2f;

    private void OnValidate()
    {
        if (_prefab == null)
        {
            throw new NullReferenceException();
        }
    }

    public Cube Generate(Vector3 scale, float force, float radius, float separationChance, Exploder exploder, Vector3 position)
    {
        Cube cube = Instantiate(_prefab, position, Quaternion.identity);

        cube.gameObject.transform.localScale = scale / _scaleReductionValue;
        cube.Initialize(force * _explosionForceMultiplier, radius * _explosionRadiusMultiplier, separationChance, exploder, this);

        if (cube.TryGetComponent(out MeshRenderer meshRenderer))
            meshRenderer.material.color = _colors[UnityEngine.Random.Range(0, _colors.Count - 1)];

        return cube;
    }
}
