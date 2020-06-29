using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private float lifetime;

    private float startTime;
    private float damageModifier = 1.0f;

    public int Damage(){
        return (int) Mathf.Ceil(this.damage * damageModifier);
    }

    public void setDamageModifier(float modifier){
        damageModifier = modifier;
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.fixedTime;
    }

    void OnTriggerEnter2D(Collider2D col){

        GameObject hitObject = col.gameObject;

        if(hitObject.tag == "Monster"){
            hitObject.GetComponent<Monster>().hitFor(Damage());
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
