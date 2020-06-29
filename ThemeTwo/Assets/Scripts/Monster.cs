using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;

    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip oof;
    
    protected Player[] targets;
    protected GameManager gameManager;

    protected int initialHealth;

    public void setGameManager(GameManager gameManager){
        this.gameManager = gameManager;
    }

    public void setHealthModifier(float modifier){
        health = (int) Mathf.Ceil(health * modifier);
        initialHealth = (int) Mathf.Ceil(initialHealth * modifier);
    }

    public void setTargets(Player[] targets){
        this.targets = targets;
    }

    public int Damage(){
        return this.damage;
    }

    public void hitFor(int amount){
        GetComponent<AudioSource>().PlayOneShot(oof);
        health -= amount;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        initialHealth = health;
    }

    void OnTriggerEnter2D(Collider2D col){

        GameObject hit = col.gameObject;

        if(hit.tag == "Player"){
            GetComponent<AudioSource>().PlayOneShot(attack);
            hit.GetComponent<Player>().hitFor(damage);
        }

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(health <= 0){
            GetComponent<AudioSource>().PlayOneShot(oof);
            GetComponent<Animator>().SetBool("dead", true);
            gameManager.addPoints(initialHealth);
            Destroy(gameObject);
        }

    }
}
