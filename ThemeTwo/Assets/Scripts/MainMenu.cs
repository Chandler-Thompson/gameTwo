using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public GameObject scoreboard;
    public AudioClip selection;

    void Start(){
        scoreboard.GetComponent<TextMeshProUGUI>().text = ""+ScoreKeeper.SCORE;
    }

    void Update(){
        scoreboard.GetComponent<TextMeshProUGUI>().text = ""+ScoreKeeper.SCORE;
    }

    public void PlayGame()
    {
        GetComponent<AudioSource>().PlayOneShot(selection);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        GameManager.isPlaying = true;
        GameManager.startTime = Time.fixedTime;
    }

    public void Test(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void QuitGame()
    {
        GetComponent<AudioSource>().PlayOneShot(selection);
        Application.Quit();
    }
}
