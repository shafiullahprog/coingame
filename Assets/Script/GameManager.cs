using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public int coins;
    public Text coinsCollected, activeScore;
    public Text highestScore;

    private int highScore = 0;

    private void Update() {
        coinsCollected.text = "No Of Coins " + coins.ToString();

        highScore = coins;
        activeScore.text = "Active Score: " +highScore.ToString();

        if(PlayerPrefs.GetInt("score") <= highScore)
        {
            PlayerPrefs.SetInt("score", highScore);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game End");
        Time.timeScale = 0;
        StoreHighScore();
    }

    public void StoreHighScore()
    {
        highestScore.text = "High Score: " + PlayerPrefs.GetInt("score");
    }


    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

   
}
