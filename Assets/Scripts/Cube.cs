using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private CubeGenerator _generator;
    [SerializeField] private float explosionRadius = 10f;
    [SerializeField] private float explosionForce = 10f;

    private int _minRandomCubes = 2;
    private int _maxRandomCubes = 6;

    public void DevideCube()
    {
        List<Cube> cubes = new List<Cube>();

        for (int i = 0; i < GetNumberOfCubes(); i++)
        {
            cubes.Add(_generator.Generate(transform.localScale));
        }

        Explode(cubes);

        Destroy(gameObject);
    }

    private void Explode(List<Cube> cubes)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i].gameObject.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }

    private int GetNumberOfCubes() => Random.Range(_minRandomCubes, _maxRandomCubes);
}
