using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Session : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;

        void Awake()
    {
        int numOfSessions = FindObjectsOfType<Session>().Length;
        playerLives = PlayerPrefs.GetInt("lives", 3);
        if (numOfSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        Camera.main.BroadcastMessage("SetLivesText", playerLives - 1);
        PlayerPrefs.SetInt("lives", PlayerPrefs.GetInt("lives", 3) - 1);
        if (playerLives > 1)
        {
            StartCoroutine(SubtractLife());
        }
        else
        {
            StartCoroutine(GameOverSequence());
        }
    }

    private IEnumerator SubtractLife()
    {
        playerLives = playerLives - 1;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(currentScene);
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }

    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(1.0f);

        if(PlayerPrefs.GetInt("score", 0) > PlayerPrefs.GetInt("highScore", 0))
        {
            var highScoreText = GameObject.Find("GameOverScreen").transform.Find("highScoreText").gameObject;
            highScoreText.SetActive(true);
            highScoreText.GetComponent<Text>().text = "New High Score: " + PlayerPrefs.GetInt("score", 0).ToString();
            

            PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("score"));
        }
        GameObject.Find("GameOverScreen").GetComponent<Canvas>().enabled = true;

        Destroy(gameObject);
    }
}
