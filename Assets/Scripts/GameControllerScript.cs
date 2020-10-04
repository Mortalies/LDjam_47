using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public int maxScore = 10;
    private int score;
    public Image scoreImg;

    private void Start()
    {
        score = 0;
        ChangeScore(0);
    }

    public void ChangeScore(int _score)
    {
        score = score + _score;
        float fillFloat = (float)score / maxScore;
        scoreImg.fillAmount = fillFloat; // изменение картинки счета
        if(score >= maxScore)
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
