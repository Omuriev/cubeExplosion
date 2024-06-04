using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeGenerator _generator;
    [SerializeField] private float _explosionRadius = 50f;
    [SerializeField] private float _explosionForce = 50f;
    [SerializeField] private float _separationChance;

    private float _maxSeparationChance = 100f;
    private float _chanceReductionValue = 2f;
    private int _minRandomCubes = 2;
    private int _maxRandomCubes = 6;
    private Rigidbody _rigidbody;

    public Rigidbody CubeRigidbody => _rigidbody;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    public void Initialize(float force, float radius, float separationChance)
    {
        if (force > _explosionForce)
            _explosionForce = force;

        if (radius > _explosionRadius)
            _explosionRadius = radius;

        _separationChance = separationChance;
    }

    public void Destroy()
    {
        if (TrySeparateCube() == true)
        {
            _separationChance /= _chanceReductionValue;
            Divide();
        }
        else
        {
            Explode();
        }

        Destroy(gameObject);
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                rigidbodies.Add(hit.attachedRigidbody);
        }

        return rigidbodies;
    }

    private void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
            AddExplosionForce(explodableObject);
    }

    private void Explode(List<Cube> cubes)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            AddExplosionForce(cubes[i].CubeRigidbody);
        }
    }

    private void AddExplosionForce(Rigidbody rigidbody)
    {
        rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    private void Divide()
    {
        List<Cube> cubes = new List<Cube>();

        for (int i = 0; i < GetNumberOfCubes(); i++)
        {
            cubes.Add(_generator.Generate(transform.localScale, _explosionForce, _explosionRadius, _separationChance));
        }

        Explode(cubes);
    }

    private int GetNumberOfCubes() => Random.Range(_minRandomCubes, _maxRandomCubes);

    private bool TrySeparateCube() => UnityEngine.Random.Range(0, _maxSeparationChance) <= _separationChance;
}
