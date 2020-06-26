using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        bool moveUp = Input.GetKey(KeyCode.W);// || Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Mouse Y") > 0;
        bool moveLeft = Input.GetKey(KeyCode.A);// || Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Mouse X") < 0;
        bool moveRight = Input.GetKey(KeyCode.D);// || Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Mouse X") > 0;
        bool moveDown = Input.GetKey(KeyCode.S);// || Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Mouse Y") < 0;

        //calculate movement
        Vector3 upVector = ((moveUp) ? Vector3.up:Vector3.zero);
        Vector3 leftVector = ((moveLeft) ? Vector3.left:Vector3.zero);
        Vector3 rightVector = ((moveRight) ? Vector3.right:Vector3.zero);
        Vector3 downVector = ((moveDown) ? Vector3.down:Vector3.zero);

        Vector3 direction = upVector + leftVector + rightVector + downVector;

        Vector3 playerVelocity = direction * speed * Time.deltaTime;

        float xPosition = GetComponent<Rigidbody2D>().position.x;
        float yPosition = GetComponent<Rigidbody2D>().position.y;

        GetComponent<Rigidbody2D>().position = new Vector2(xPosition + playerVelocity.x, yPosition + playerVelocity.y);

    }
}
