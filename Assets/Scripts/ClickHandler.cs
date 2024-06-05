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
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(SeparationClickCommand))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit hit, _maxDistance))
            {
                if (hit.transform.TryGetComponent(out Cube cube))
                    cube.Destroy();
            }
        }
    }
}
