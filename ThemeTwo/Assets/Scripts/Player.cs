using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //basic information of Entity
	public float speed;
    public float closestDist;
    public float furthestDist;

	public int health;

    public bool isLeft;
    public Player partner;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        bool moveUp = Input.GetKey(KeyCode.W);// || Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Mouse Y") > 0;
        bool moveLeft = Input.GetKey(KeyCode.A);// || Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Mouse X") < 0;
        bool moveRight = Input.GetKey(KeyCode.D);// || Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Mouse X") > 0;
        bool moveDown = Input.GetKey(KeyCode.S);// || Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Mouse Y") < 0;

        bool moveCloser = Input.GetKey(KeyCode.Q);
        bool moveFurther = Input.GetKey(KeyCode.E);

        //calculate movement
        Vector3 upVector = ((moveUp) ? Vector3.up:Vector3.zero);
        Vector3 leftVector = ((moveLeft) ? Vector3.left:Vector3.zero);
        Vector3 rightVector = ((moveRight) ? Vector3.right:Vector3.zero);
        Vector3 downVector = ((moveDown) ? Vector3.down:Vector3.zero);

        Vector3 closerVector = ((moveCloser && isLeft) ? Vector3.right : (moveCloser && !isLeft) ? Vector3.left : Vector3.zero);
        Vector3 furtherVector = ((moveFurther && isLeft) ? Vector3.left : (moveFurther && !isLeft) ? Vector3.right : Vector3.zero);

        //check that partner isn't too close or too far away
        float dist = Vector3.Distance(partner.transform.position, this.transform.position);

        if(dist >= furthestDist)
            furtherVector = Vector3.zero;

        if(dist <= closestDist)
            closerVector = Vector3.zero;

        Vector3 direction = upVector + leftVector + rightVector + downVector + closerVector + furtherVector;

        Vector3 playerVelocity = direction * speed * Time.deltaTime;

        float xPosition = GetComponent<Rigidbody2D>().position.x;
        float yPosition = GetComponent<Rigidbody2D>().position.y;

        GetComponent<Rigidbody2D>().position = new Vector2(xPosition + playerVelocity.x, yPosition + playerVelocity.y);

    }
}
