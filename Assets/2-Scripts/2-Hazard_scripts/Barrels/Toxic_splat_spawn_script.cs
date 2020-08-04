using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toxic_splat_spawn_script : MonoBehaviour
{
    public Explosive_barrel_script toxicBarrel;
    public GameObject toxicSplash;
    private int spawnedSplash = 0;

    void Update()
    {
        SpawnSplash();
    }

    void SpawnSplash() {
        if (toxicBarrel.blown && spawnedSplash <= 5) {
            GameObject newToxicSplash = Instantiate(toxicSplash, transform.position, Quaternion.identity);
            spawnedSplash++;
        }
    }
}
