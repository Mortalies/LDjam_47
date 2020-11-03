using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public int maxScore = 10;
    public float maxTimer;
    private int score;
    public Image scoreImg;
    //public Text timerText;
    public Image timerImg;
    private GameObject player;
    private float timer;
    private bool startTimer;
    private GameObject obj;
    public AudioClip[] music;
    public float musicVolume;
    public float parkMusicVolume;
    public GameObject[] spawnObj;
    public Image fadeImg;
    public int currentLevelInBuild;


    private void Start()
    {
        score = 0;
        ChangeScore(0);
        timer = maxTimer;
        //timerText.text = timer.ToString("#.#");
        player = GameObject.Find("Player");
        startTimer = false;
        timerImg.fillAmount = timer / maxTimer;
        PlayMusic();
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
        CheatWinCheck();
    }
    private void LoseTrash()
    {
        startTimer = false;
        player.GetComponent<Animator>().SetBool("withObj", false);
        Destroy(obj);
        ChangeScore(-1);
        Spawn();
        Spawn();

    }
    void Spawn()
    {
        spawnObj[Random.Range(0, spawnObj.Length - 1)].GetComponent<TrashSpawnerScript>().SpawnObj();
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
        StartCoroutine(Fading());

    }
    IEnumerator Fading()
    {
        fadeImg.gameObject.GetComponent<Animator>().SetBool("Fade", true);
        yield return new WaitUntil(() => fadeImg.color.a == 1);
        SceneManager.LoadScene(currentLevelInBuild + 1);

    }


    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void ChangeSound()
    {

    }
    void PlayMusic()
    {
        string levelName = SceneManager.GetActiveScene().name;
        switch (levelName)
        {
            case "Level 1":
                GetComponent<AudioSource>().clip = music[0];
                GetComponent<AudioSource>().volume = parkMusicVolume;
                break;
            case "Level 2":
                GetComponent<AudioSource>().clip = music[1];
                GetComponent<AudioSource>().volume = musicVolume;
                break;
            case "Level 3":
                GetComponent<AudioSource>().clip = music[2];
                GetComponent<AudioSource>().volume = musicVolume;
                break;
            default:
                break;
        }
        GetComponent<AudioSource>().loop = true;
        
        GetComponent<AudioSource>().Play();
    }
    void CheatWinCheck()
    {
        if (Input.GetKey(KeyCode.N))
        {
            if (Input.GetKey(KeyCode.J))
            {
                if (Input.GetKey(KeyCode.I))
                {
                    WinGame();
                }
            }
        }
    }





}
