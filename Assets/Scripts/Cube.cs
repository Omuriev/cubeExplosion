using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 50f;
    [SerializeField] private float _explosionForce = 50f;
    [SerializeField] private float _separationChance;

    private float _maxSeparationChance = 100f;
    private float _chanceReductionValue = 2f;
    private int _minRandomCubes = 2;
    private int _maxRandomCubes = 6;
    private Rigidbody _rigidbody;
    private Exploder _exploder;
    private CubeGenerator _generator;

    public Rigidbody CubeRigidbody => _rigidbody;
    public float ExplosionRadius => _explosionRadius;
    public float ExplosionForce => _explosionForce;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (_exploder == null)
        {
            _exploder = FindObjectOfType<Exploder>();
        }

        if (_generator == null)
        {
            _generator = FindObjectOfType<CubeGenerator>();
        }
    }

    public void Initialize(float force, float radius, float separationChance, Exploder exploder, CubeGenerator generator)
    {
        if (force > _explosionForce)
            _explosionForce = force;

        if (radius > _explosionRadius)
            _explosionRadius = radius;

        _separationChance = separationChance;
        _exploder = exploder;
        _generator = generator;
    }

    public void Destroy()
    {
        if (CanSeparateCube())
        {
            _separationChance /= _chanceReductionValue;
            Divide();
        }
        else
        {
            _exploder.Explode(GetExplodableObjects(), transform.position);
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
            cubes.Add(_generator.Generate(transform.localScale, _explosionForce, _explosionRadius, _separationChance, _exploder, transform.position));
        }

        _exploder.Explode(cubes, transform.position);
    }

    private int GetNumberOfCubes() => Random.Range(_minRandomCubes, _maxRandomCubes);

    private bool CanSeparateCube() => UnityEngine.Random.Range(0, _maxSeparationChance) <= _separationChance;
}
