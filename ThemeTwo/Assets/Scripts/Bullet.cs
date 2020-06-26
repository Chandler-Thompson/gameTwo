using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float startTime;
    [SerializeField]
    private float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        //destroy bullet after a bit
        if(Time.fixedTime - startTime >= lifetime){
            Destroy(gameObject);
        }
    }
}
