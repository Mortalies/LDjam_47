using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllerScript : MonoBehaviour
{
    private GameObject spawn;
    public GameObject bg;
    public GameObject lastBackGround;
    public AudioClip musicMenu;
    public float musicVolume = 1f;
    public Image fadeImg;
    public int currentLevelInBuild;

    void Start()
    {
        PlayMusic();
    }
    public void SpawnBackGround()
    {
        print("spawn");
        GameObject newBg = Instantiate(bg, lastBackGround.transform.Find("SpawnPoint").transform.position, lastBackGround.transform.rotation);
        lastBackGround = newBg;
    }
    public void LoadLevel(string levelName)
    {
        //SceneManager.LoadScene(levelName);
        StartCoroutine(Fading());
    }
    void PlayMusic()
    {
        GetComponent<AudioSource>().clip = musicMenu;
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().volume = musicVolume;
        GetComponent<AudioSource>().Play();
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
}
