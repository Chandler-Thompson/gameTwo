using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float separationSpeed;
    [SerializeField] private float closestDist;
    [SerializeField] private float furthestDist;

    [SerializeField] private Player leftPlayer;
    [SerializeField] private Player rightPlayer;

    private float playerDist = 1;//tracked from left player's perspective

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        /*
        
            Gravitate towards position

        */

        /*
        
            Rotate towards mouse

        */

        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(
                                                        mouse.x, 
                                                        mouse.y,
                                                        mouse.z));
        Vector3 forward = -mouseWorld + this.transform.position;

        var newRotation = Quaternion.LookRotation(forward, Vector3.forward);
        newRotation.y = 0.0f;
        newRotation.x = 0.0f;

        this.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);

        /*
        
            Q E Movement

        */

        bool moveCloser = Input.GetKey(KeyCode.Q);
        bool moveFurther = Input.GetKey(KeyCode.E);

        //closer and further in terms of the left player
        Vector3 closerVector = ((moveCloser) ? Vector3.right : Vector3.zero);
        Vector3 furtherVector = ((moveFurther) ? Vector3.left : Vector3.zero);

        //check that partner isn't too close or too far away, only need to check one
        float dist = Vector3.Distance(leftPlayer.transform.position, this.transform.position);

        if(dist >= furthestDist)
            furtherVector = Vector3.zero;

        if(dist <= closestDist)
            closerVector = Vector3.zero;

        //move closer or further away
        Vector3 leftDirection = furtherVector + closerVector;
        Vector3 rightDirection = -(furtherVector + closerVector);//invert vectors for rightPlayer

        Vector2 leftVelocity = leftDirection * separationSpeed * Time.deltaTime;
        Vector2 rightVelocity = rightDirection * separationSpeed * Time.deltaTime;

        Rigidbody2D leftBody = leftPlayer.GetComponent<Rigidbody2D>();
        Rigidbody2D rightBody = rightPlayer.GetComponent<Rigidbody2D>();

        leftBody.MovePosition(leftBody.position + leftVelocity * Time.deltaTime);
        rightBody.MovePosition(rightBody.position + rightVelocity * Time.deltaTime);

        /*
        
            WASD Movement
        
        */

        bool moveUp = Input.GetKey(KeyCode.W);// || Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Mouse Y") > 0;
        bool moveLeft = Input.GetKey(KeyCode.A);// || Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Mouse X") < 0;
        bool moveRight = Input.GetKey(KeyCode.D);// || Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Mouse X") > 0;
        bool moveDown = Input.GetKey(KeyCode.S);// || Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Mouse Y") < 0;

        //calculate movement
        Vector3 upVector = ((moveUp) ? Vector3.up:Vector3.zero);
        Vector3 leftVector = ((moveLeft) ? Vector3.left:Vector3.zero);
        Vector3 rightVector = ((moveRight) ? Vector3.right:Vector3.zero);
        Vector3 downVector = ((moveDown) ? Vector3.down:Vector3.zero);

        Vector3 moveDirection = upVector + leftVector + rightVector + downVector;

        Vector3 playerVelocity = moveDirection * movementSpeed;

        float xPosition = GetComponent<Rigidbody2D>().position.x;
        float yPosition = GetComponent<Rigidbody2D>().position.y;

        GetComponent<Rigidbody2D>().velocity = playerVelocity;

        // GetComponent<Rigidbody2D>().position = new Vector2(xPosition + playerVelocity.x, yPosition + playerVelocity.y);
        
        Vector2 leftVel = new Vector2(leftBody.position.x + playerVelocity.x, leftBody.position.y + playerVelocity.y);
        // leftBody.MovePosition(leftVel);
        leftBody.velocity = playerVelocity;

        Vector2 rightVel = new Vector2(rightBody.position.x + playerVelocity.x, rightBody.position.y + playerVelocity.y);
        // rightBody.MovePosition(rightVel);
        rightBody.velocity = playerVelocity;

    }
}
