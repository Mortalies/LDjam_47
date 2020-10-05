using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitlesScript : MonoBehaviour
{
    private void Start()
    {
        Invoke("LoadLevelOne", 22);
    }
    void LoadLevelOne()
    {
        SceneManager.LoadScene("Level 1");
    }
}
