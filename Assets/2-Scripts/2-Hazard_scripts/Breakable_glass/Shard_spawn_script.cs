using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard_spawn_script : MonoBehaviour
{
    public GameObject[] shards;
    public Breakable_glass_script glass;
    private bool spawned = false;
    
    void Update()
    {
        SpawnShard();
    }

    void SpawnShard() {
        if (!spawned && glass.broken) {
            Instantiate(shards[Random.Range(0, 7)], transform.position, Quaternion.identity);
            spawned = true;
        }
    }
}
