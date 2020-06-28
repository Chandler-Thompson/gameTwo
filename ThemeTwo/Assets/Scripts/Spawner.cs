using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private float spawnFrequency;
    [SerializeField] private Monster monster;
    [SerializeField] private Player[] targets;

    private float lastSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //spawn the monster every <spawnFrequency> seconds
        if(Time.fixedTime - lastSpawn >= spawnFrequency){
            lastSpawn = Time.fixedTime;
            Monster newMonster = Instantiate(monster, transform.position, Quaternion.identity);
            newMonster.setTargets(targets);
        }

    }
}
