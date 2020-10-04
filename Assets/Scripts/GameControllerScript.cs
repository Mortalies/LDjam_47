using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public int maxScore = 10;
    public float maxTimer = 10f;
    private int score;
    public Image scoreImg;
    //public Text timerText;
    public Image timerImg;
    private GameObject player;
    private float timer;
    private bool startTimer;
    private GameObject obj;


    private void Start()
    {
        score = 0;
        ChangeScore(0);
        timer = maxTimer;
        //timerText.text = timer.ToString("#.#");
        player = GameObject.Find("Player");
        startTimer = false;
        timerImg.fillAmount = timer / maxTimer;
    }
    public void StartTimer()
    {
        startTimer = true;
        obj = player.GetComponent<CharacterScript>().ReturnJoinedObject();
    }
    public GameObject ReturnObj()
    {
        return obj;
    }
    public bool ReturnStartTimer()
    {
        return startTimer;
    }
    private void Update()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
            timerImg.fillAmount = timer / maxTimer;
            //timerText.text = timer.ToString("#.#");

        }
        if (timer <= 0)
        {
            LoseTrash();
            
        }
    }
    private void LoseTrash()
    {
        startTimer = false;
        Destroy(obj);
        ChangeScore(-1);
        
    }



    public void ChangeScore(int _score)
    {
        score = score + _score;
        float fillFloat = (float)score / maxScore;
        scoreImg.fillAmount = fillFloat; // изменение картинки счета

        startTimer = false; //обнуление таймера
        timer = maxTimer;
        timerImg.fillAmount = timer / maxTimer;
        //timerText.text = timer.ToString("#.#");

        if (score <= 0)
        {
            score = 0;
        }
        if (score >= maxScore)
        {
            WinGame();
        }
    }
    void WinGame()
    {
        print("You win");
    }


    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    


}
