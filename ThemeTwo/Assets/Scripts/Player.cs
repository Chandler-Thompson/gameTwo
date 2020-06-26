using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerMovement
{

    //basic information of Entity
	public float speed;
    public float closestDist;
    public float furthestDist;

	public int health;

    public bool isLeft;
    public GameObject followPoint;

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
    public override void Update()
    {
        base.Update();

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

        Vector3 playerVelocity = direction * speed * Time.deltaTime;

        float xPosition = GetComponent<Rigidbody2D>().position.x;
        float yPosition = GetComponent<Rigidbody2D>().position.y;

        GetComponent<Rigidbody2D>().position = new Vector2(xPosition + playerVelocity.x, yPosition + playerVelocity.y);

    }
}
