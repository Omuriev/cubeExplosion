using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeGenerator _generator;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private float _explosionRadius = 50f;
    [SerializeField] private float _explosionForce = 50f;
    [SerializeField] private float _separationChance;

    private float _maxSeparationChance = 100f;
    private float _chanceReductionValue = 2f;
    private int _minRandomCubes = 2;
    private int _maxRandomCubes = 6;
    private Rigidbody _rigidbody;

    public Rigidbody CubeRigidbody => _rigidbody;
    public float ExplosionRadius => _explosionRadius;
    public float ExplosionForce => _explosionForce;

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
        if (CanSeparateCube() == true)
        {
            _separationChance /= _chanceReductionValue;
            Divide();
        }
        else
        {
            _exploder.Explode(GetExplodableObjects());
        }

        Destroy(gameObject);
    }

    private List<Cube> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Cube> cubes = new List<Cube>();

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Cube cube))
                cubes.Add(cube);
        }

        return cubes;
    }

    private void Divide()
    {
        List<Cube> cubes = new List<Cube>();

        int numberOfCubes = GetNumberOfCubes();

        for (int i = 0; i < numberOfCubes; i++)
        {
            cubes.Add(_generator.Generate(transform.localScale, _explosionForce, _explosionRadius, _separationChance));
        }

        _exploder.Explode(cubes);
    }

    private int GetNumberOfCubes() => Random.Range(_minRandomCubes, _maxRandomCubes);

    private bool CanSeparateCube() => UnityEngine.Random.Range(0, _maxSeparationChance) <= _separationChance;
}
