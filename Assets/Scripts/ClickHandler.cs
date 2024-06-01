using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private const int SeparationClickCommand = 0;

    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxDistance = 100f;

    private Ray _ray;
    private float _currentSeparationChance;
    private float _maxSeparationChance = 100f;
    private float _chanceReductionValue = 2f;

    private void Start()
    {
        _currentSeparationChance = _maxSeparationChance;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(SeparationClickCommand))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit hit, _maxDistance))
            {
                if (hit.transform.TryGetComponent(out Cube cube))
                {
                    if (TrySeparateCube() == true)
                        cube.DevideCube();
                    else
                        cube.Explode();

                    _currentSeparationChance /= _chanceReductionValue;
                }
            }
        }
    }

    private bool TrySeparateCube() => UnityEngine.Random.Range(0, _maxSeparationChance) <= _currentSeparationChance;
}
