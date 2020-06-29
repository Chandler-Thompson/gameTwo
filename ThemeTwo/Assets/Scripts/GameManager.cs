using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Player[] players;
    public GameObject[] turrets;

    private int points;
    private float lastTimePointsGiven = 0.0f;


    [HideInInspector] public static float startTime = 0.0f;
    [HideInInspector] public static bool isPlaying = false;

    public void addPoints(int amount){
        points += amount;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.fixedTime - lastTimePointsGiven > 10 && isPlaying){
            lastTimePointsGiven = Time.fixedTime;
            points++;
        }
        
        bool allDead = true;
        for(int i = 0; i < players.Length; i++){
            if(!players[i].isDead()){
                allDead = false;
            }
        }

        if(allDead){
            ScoreKeeper.SCORE = points;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

    }
}
