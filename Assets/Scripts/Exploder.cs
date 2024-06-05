using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public void Explode(List<Cube> cubes)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            AddExplosionForce(cubes[i].GetComponent<Rigidbody>(), cubes[i].ExplosionForce, cubes[i].ExplosionRadius);
        }
    }

    private void AddExplosionForce(Rigidbody rigidbody, float explosionForce, float explosionRadius)
    {

        rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
    }
}
