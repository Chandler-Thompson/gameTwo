using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{

    private Player currTarget;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //randomly select target
        int targetIndex = Random.Range(0, this.targets.Length);
        currTarget = targets[targetIndex];
    }

    // Update is called once per frame
    protected override void Update()
    {

        base.Update();

        if(health == 0)
            Debug.Log("Dead Slime!");

        /*
            
            Slimes run towards randomly selected target

        */

        Vector3 direction = currTarget.transform.position - this.transform.position;

        Vector3 velocity = direction * speed * Time.deltaTime;

        Rigidbody2D body = GetComponent<Rigidbody2D>();

        float x = body.position.x;
        float y = body.position.y;

        body.position = new Vector2(x + velocity.x, y + velocity.y);

        if(currTarget.isDead()){
            //randomly select new target (could select dead target, will just idle then)
            int targetIndex = Random.Range(0, this.targets.Length);
            currTarget = targets[targetIndex];
        }

    }
}
