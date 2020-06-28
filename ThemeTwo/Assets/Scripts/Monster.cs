using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    [SerializeField] protected Player[] targets;

    public int Damage(){
        return this.damage;
    }

    public void hitFor(int amount){
        health -= amount;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        
        GameObject hit = col.gameObject;

        if(hit.tag == "Player"){
            Debug.Log("Squelch!");
            hit.GetComponent<Player>().hitFor(damage);
        }

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
        Debug.Log("Monster: " + health);

        if(health <= 0)
            Destroy(gameObject);

    }
}
