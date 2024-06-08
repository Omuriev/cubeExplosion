using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public void Explode(List<Cube> cubes, Vector3 explosionSite)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            AddExplosionForce(cubes[i].GetComponent<Rigidbody>(), cubes[i].ExplosionForce, cubes[i].ExplosionRadius, explosionSite);
        }
    }

    private void AddExplosionForce(Rigidbody rigidbody, float explosionForce, float explosionRadius, Vector3 explosionSite)
    {
        rigidbody.AddExplosionForce(explosionForce, explosionSite, explosionRadius);
    }
}
