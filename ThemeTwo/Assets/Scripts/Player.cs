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

    private GameObject followPoint;

    public bool isRight(){
        return !isLeft;
    }

    public void takeDamage(int amount){
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

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0)){
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
