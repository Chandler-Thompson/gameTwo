using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //basic information of Entity
    [SerializeField]
    private float closestDist;
    [SerializeField]
    private float furthestDist;
    [SerializeField]
    private float separationSpeed;
    [SerializeField]
	private int health;
    [SerializeField]
    private bool isLeft;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float bulletSpeed;

    private GameObject followPoint;

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
        followPoint = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        //Get Closer and Separate

        bool moveCloser = Input.GetKey(KeyCode.Q);
        bool moveFurther = Input.GetKey(KeyCode.E);

        Vector3 closerVector = ((moveCloser && isLeft) ? Vector3.right : (moveCloser && !isLeft) ? Vector3.left : Vector3.zero);
        Vector3 furtherVector = ((moveFurther && isLeft) ? Vector3.left : (moveFurther && !isLeft) ? Vector3.right : Vector3.zero);

        //check that partner isn't too close or too far away
        float dist = Vector3.Distance(followPoint.transform.position, this.transform.position);

        if(dist >= furthestDist)
            furtherVector = Vector3.zero;

        if(dist <= closestDist)
            closerVector = Vector3.zero;

        //move closer or further away
        Vector3 direction = furtherVector + closerVector;

        Vector3 playerVelocity = direction * separationSpeed * Time.deltaTime;

        Debug.Log(direction +"|"+ playerVelocity +"|");

        float xPosition = GetComponent<Rigidbody2D>().position.x;
        float yPosition = GetComponent<Rigidbody2D>().position.y;

        GetComponent<Rigidbody2D>().position = new Vector2(xPosition + playerVelocity.x, yPosition + playerVelocity.y);

        if(Input.GetMouseButtonDown(0)){
            //Pew Pew angle
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 shootDirection = (Vector2)((worldMousePos - transform.position));
            shootDirection.Normalize();

            //Create the bullet
            GameObject bulletInstance = (GameObject)Instantiate(
                bullet, 
                transform.position + (Vector3)(direction*0.5f),
                Quaternion.identity);

            //Adds velocity to the bullet
            bulletInstance.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
        }

    }
}
