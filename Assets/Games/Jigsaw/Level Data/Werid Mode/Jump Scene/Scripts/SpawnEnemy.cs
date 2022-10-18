using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public GameObject enemytoSpawn;
    public GameObject ground;

    public GameObject lastSpawned;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBadGuy", 2f,2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBadGuy() 
    {

     lastSpawned = Instantiate(enemytoSpawn,transform);
     lastSpawned.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
     lastSpawned.transform.parent = ground.transform;
    
    }
}
