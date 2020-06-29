using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private float gravitationSpeed = 1;
    [SerializeField] private float closestDist;
    [SerializeField] private float furthestDist;

    [SerializeField] private Player leftPlayer;
    [SerializeField] private Player rightPlayer;
    [SerializeField] private GameObject leftTurret;
    [SerializeField] private GameObject rightTurret;

    private Animator leftAnimator;
    private Animator rightAnimator;

    // Start is called before the first frame update
    void Start()
    {
        leftAnimator = leftPlayer.GetComponent<Animator>();
        rightAnimator = rightPlayer.GetComponent<Animator>();
    }

    void Update(){
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 newLeftDirection = Vector3.zero;
        Vector3 newRightDirection = Vector3.zero;

        Vector3 newStaticLeftDirection = Vector3.zero;
        Vector3 newStaticRightDirection = Vector3.zero;

        Vector3 newDirection = Vector3.zero;

        Rigidbody2D body = GetComponent<Rigidbody2D>();

        Rigidbody2D leftBody = leftPlayer.GetComponent<Rigidbody2D>();
        Rigidbody2D rightBody = rightPlayer.GetComponent<Rigidbody2D>();

        Transform staticLeftTrans = leftTurret.transform;
        Transform staticRightTrans = rightTurret.transform;

        Vector3 staticLeftPos = staticLeftTrans.position;
        Vector3 staticRightPos = staticRightTrans.position; 

        Vector3 leftPos = leftBody.position;
        Vector3 rightPos = rightBody.position;

        Vector3 pos = body.position;

        /*
        
            Gravitate towards position

        */

        var direction = Vector3.zero;
        float leftDist = Vector3.Distance(staticLeftPos, leftPos);
         if(leftDist > 0.05)
         {
            newLeftDirection += leftTurret.transform.position - leftPos;
         }

        float rightDist = Vector3.Distance(staticRightPos, rightPos);
        if(rightDist > 0.05)
        {
            newRightDirection += rightTurret.transform.position - rightPos;
        }

        /*
        
            Rotate towards mouse

        */

        // Vector3 mouse = Input.mousePosition;
        // Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(
        //                                                 mouse.x, 
        //                                                 mouse.y,
        //                                                 mouse.z));
        // Vector3 forward = -mouseWorld + this.transform.position;

        // var newRotation = Quaternion.LookRotation(forward, Vector3.forward);
        // newRotation.y = 0.0f;
        // newRotation.x = 0.0f;

        // this.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);

        /*
        
            Q E Movement

        */

        //check that static position isn't too close or far away, only need to check one
        float distance = Vector3.Distance(staticLeftPos, staticRightPos);
        
        leftPlayer.setPartnerDistance(distance);
        rightPlayer.setPartnerDistance(distance);

        if(Input.GetKey(KeyCode.Q) && distance >= closestDist){//move closer if possible
            newStaticLeftDirection += Vector3.right;
            newStaticRightDirection += Vector3.left;
        }

        if(Input.GetKey(KeyCode.E) && distance < furthestDist){//move further if possible
            newStaticLeftDirection += Vector3.left;
            newStaticRightDirection += Vector3.right;
        }

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

        newDirection += moveDirection;
        newLeftDirection += moveDirection;
        newRightDirection += moveDirection;
        
        if(newStaticLeftDirection == Vector3.zero)
            newStaticLeftDirection += moveDirection;

        if(newStaticRightDirection == Vector3.zero)
            newStaticRightDirection += moveDirection;

        /*

            Calculate and set new values
        
        */

        Vector3 newPos = pos + (newDirection.normalized * movementSpeed*2 * Time.deltaTime);

        Vector3 newLeftPos = leftPos + (newLeftDirection.normalized * gravitationSpeed * Time.deltaTime);
        Vector3 newRightPos = rightPos + (newRightDirection.normalized * gravitationSpeed * Time.deltaTime);

        Vector3 newStaticLeftPos = staticLeftPos + (newStaticLeftDirection.normalized * movementSpeed * Time.deltaTime);
        Vector3 newStaticRightPos = staticRightPos + (newStaticRightDirection.normalized * movementSpeed * Time.deltaTime);

        if(!leftPlayer.isDead() && !rightPlayer.isDead())
            body.position = newPos;

        if(!leftPlayer.isDead()){
            leftBody.position = newLeftPos;
            staticLeftTrans.position = newStaticLeftPos;
        }

        if(!rightPlayer.isDead()){
            rightBody.position = newRightPos;
            staticRightTrans.position = newStaticRightPos;
        }

        updateAnimations(newLeftDirection, newRightDirection);
        
    }

    private void updateAnimations(Vector3 leftDirection, Vector3 rightDirection){

        if(!leftPlayer.isDead()){
            if(leftDirection.y > 0.01){
                leftAnimator.SetBool("walkingRight", false);
                leftAnimator.SetBool("walkingLeft", false);
                leftAnimator.SetBool("walkingUp", true);        
                leftAnimator.SetBool("walkingDown", false);
            } else if(leftDirection.y < -0.01){
                leftAnimator.SetBool("walkingRight", false);
                leftAnimator.SetBool("walkingLeft", false);
                leftAnimator.SetBool("walkingUp", false);        
                leftAnimator.SetBool("walkingDown", true);
            } else if(leftDirection.x > 0.01){
                leftAnimator.SetBool("walkingRight", true);
                leftAnimator.SetBool("walkingLeft", false);
                leftAnimator.SetBool("walkingUp", false);        
                leftAnimator.SetBool("walkingDown", false);
            } else if(leftDirection.x < -0.01){
                leftAnimator.SetBool("walkingRight", false);
                leftAnimator.SetBool("walkingLeft", true);
                leftAnimator.SetBool("walkingUp", false);        
                leftAnimator.SetBool("walkingDown", false);
            } else{
                leftAnimator.SetBool("walkingRight", false);
                leftAnimator.SetBool("walkingLeft", false);
                leftAnimator.SetBool("walkingUp", false);        
                leftAnimator.SetBool("walkingDown", false);
            }
        }else{
            leftAnimator.SetBool("walkingRight", false);
            leftAnimator.SetBool("walkingLeft", false);
            leftAnimator.SetBool("walkingUp", false);        
            leftAnimator.SetBool("walkingDown", false);
        }

        if(!rightPlayer.isDead()){

            if(rightDirection.y > 0.01){
                rightAnimator.SetBool("walkingRight", false);
                rightAnimator.SetBool("walkingLeft", false);
                rightAnimator.SetBool("walkingUp", true);        
                rightAnimator.SetBool("walkingDown", false);
            } else if(rightDirection.y < -0.01){
                rightAnimator.SetBool("walkingRight", false);
                rightAnimator.SetBool("walkingLeft", false);
                rightAnimator.SetBool("walkingUp", false);        
                rightAnimator.SetBool("walkingDown", true);
            } else if(rightDirection.x > 0.01){
                rightAnimator.SetBool("walkingRight", true);
                rightAnimator.SetBool("walkingLeft", false);
                rightAnimator.SetBool("walkingUp", false);        
                rightAnimator.SetBool("walkingDown", false);
            } else if(rightDirection.x < -0.01){
                rightAnimator.SetBool("walkingRight", false);
                rightAnimator.SetBool("walkingLeft", true);
                rightAnimator.SetBool("walkingUp", false);        
                rightAnimator.SetBool("walkingDown", false);
            } else{
                rightAnimator.SetBool("walkingRight", false);
                rightAnimator.SetBool("walkingLeft", false);
                rightAnimator.SetBool("walkingUp", false);        
                rightAnimator.SetBool("walkingDown", false);
            }

        }else{
            rightAnimator.SetBool("walkingRight", false);
            rightAnimator.SetBool("walkingLeft", false);
            rightAnimator.SetBool("walkingUp", false);        
            rightAnimator.SetBool("walkingDown", false);
        }
        
    }

}
