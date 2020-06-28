using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private float lifetime;

    private float startTime;

    public int Damage(){
        return this.damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.fixedTime;
    }

    void OnTriggerEnter2D(Collider2D col){

        GameObject hitObject = col.gameObject;

        if(hitObject.tag == "Monster"){
            Debug.Log("Ping");
            hitObject.GetComponent<Monster>().hitFor(damage);
            Destroy(gameObject);
        }
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
