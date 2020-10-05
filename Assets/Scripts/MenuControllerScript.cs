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

    public void SpawnBackGround()
    {
        print("spawn");
        GameObject newBg = Instantiate(bg, lastBackGround.transform.Find("SpawnPoint").transform.position, lastBackGround.transform.rotation);
        lastBackGround = newBg;
    }
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
