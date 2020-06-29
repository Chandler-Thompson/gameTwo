using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //basic information of Entity
    [SerializeField] private int health;
    [SerializeField] private bool isLeft;
    [SerializeField] private Bullet bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float firerate;
    [SerializeField] private GameObject turret;
    [SerializeField] private Sprite deadTurret;
    [SerializeField] private AudioClip gunshot;
    [SerializeField] private AudioClip oof;

    private float prevBulletFired = 0.0f;
    private float distFromPartner = 1.0f;

    private GameObject followPoint;

    public void setPartnerDistance(float dist){
        this.distFromPartner = dist;
    }

    public void hitFor(int amount){
        GetComponent<AudioSource>().PlayOneShot(oof);
		health -= amount;
	}

	protected void die(){
		health = 0;
	}

    public bool isDead() {
        return health <= 0;
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

        /*
        
            Pew Pew

        */
        if(Input.GetMouseButton(0) && Time.fixedTime - prevBulletFired >= firerate && health > 0){

            prevBulletFired = Time.fixedTime;

            //Pew Pew angle
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 shootDirection = (Vector2)((worldMousePos - turret.transform.position));
            shootDirection.Normalize();

            //Create the bullet
            GameObject bulletInstance = Instantiate(
                bullet.gameObject, 
                turret.transform.position + (Vector3)(shootDirection*0.5f),
                Quaternion.identity);

            bulletInstance.GetComponent<Bullet>().setDamageModifier(distFromPartner);
            bulletInstance.transform.localScale = Vector3.one * (distFromPartner/2 + 1);
            //Adds velocity to the bullet
            bulletInstance.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;

            GetComponent<AudioSource>().PlayOneShot(gunshot);

        }

        /*
        
            Deaded

        */

        if(health <= 0){
            GetComponent<Animator>().SetBool("dead", true);
            GetComponent<Animator>().SetBool("walkingUp", false);
            GetComponent<Animator>().SetBool("walkingDown", false);
            GetComponent<Animator>().SetBool("walkingLeft", false);
            GetComponent<Animator>().SetBool("walkingRight", false);
            turret.GetComponent<SpriteRenderer>().sprite = deadTurret;
        }

    }
}
