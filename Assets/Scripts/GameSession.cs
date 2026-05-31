using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int scoreNumber = 0;


    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    public void Awake()
    {
        
        int numberGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = scoreNumber.ToString();
        
    }

    public void AddToScore (int pointsToAdd)
    {
        scoreNumber += pointsToAdd;
        scoreText.text = scoreNumber.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    void ResetGameSession()
    {
        FindFirstObjectByType<ScenePersist>().ResetScenePersist(); 
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }


    


}
