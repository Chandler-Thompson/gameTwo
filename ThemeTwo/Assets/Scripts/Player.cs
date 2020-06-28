using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //basic information of Entity
    [SerializeField] private int health;
    [SerializeField] private bool isLeft;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float firerate;

    private float prevBulletFired = 0.0f;

    private GameObject followPoint;

    public void hitFor(int amount){
		health -= amount;
	}

	protected void die(){
		health = 0;
	}

    public bool isDead() {
        return health == 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        followPoint = this.transform.parent.gameObject;
    }

    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D col)
    {

        if(col == null)
            return;

        //get the object collider is attached to
        GameObject hitBy = col.gameObject;

        //if hit by bullet, take damage
        if(hitBy.tag == "Monster"){
            Monster monster = null;
            hitBy.TryGetComponent<Monster>(out monster);

            this.hitFor(monster.Damage());
        }
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("Player: "+health);

        /*
        
            Pew Pew

        */
        if(Input.GetMouseButton(0) && Time.fixedTime - prevBulletFired >= firerate){

            prevBulletFired = Time.fixedTime;

            //Pew Pew angle
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 shootDirection = (Vector2)((worldMousePos - transform.position));
            shootDirection.Normalize();

            //Create the bullet
            GameObject bulletInstance = (GameObject)Instantiate(
                bullet, 
                transform.position + (Vector3)(shootDirection*0.5f),
                Quaternion.identity);

            //Adds velocity to the bullet
            bulletInstance.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;

        }

    }
}
