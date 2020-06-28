using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private float spawnFrequency;
    [SerializeField] private Monster monster;

    private float lastSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.fixedTime - lastSpawn >= spawnFrequency){

        }

    }
}
