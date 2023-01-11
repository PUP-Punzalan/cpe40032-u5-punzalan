using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Component variables
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI liveText;

    // Game object variables
    public List<GameObject> targets;
    public GameObject titleScreen;
    public GameObject gameOverScreen;

    // Basic data type variables
    public bool isGameActive;
    public int score;
    public int live;
    private float spawnRate = 1;

    // This will run every frame/second but with rate/cooldown
    IEnumerator SpawnTarget()
    {
        // run if game is active
        while (isGameActive)
        {
            // "yield" is for pausing the coroutine
            yield return new WaitForSeconds(spawnRate);

            // after the pause this will run
            // store a random object index on a variable and spawn it
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    // Custom method to update score
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // Custom method to update live
    public void UpdateLive(int liveToAdd)
    {
        live += liveToAdd;
        liveText.text = "Lives: " + live;
    }

    // Custom method to check the status of the game
    public void CheckStatus()
    {
        // make score not go lower than 0
        if (score < 0)
        {
            score = 0;
            scoreText.text = "Score: " + score;
        }

        // game over when live reaches 0
        if (live <= 0)
        {
            live = 0;
            liveText.text = "Lives: " + live;
            isGameActive = false;;
            gameOverScreen.gameObject.SetActive(true);
        }
    }

    // Custom method to restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Custom method to start the game
    public void StartGame(int difficulty)
    {
        // starting value
        isGameActive = true;
        score = 0;
        live = 3;
        spawnRate /= difficulty;

        // calls the method every frame/seconds (?)
        StartCoroutine(SpawnTarget());

        // update score and live
        UpdateScore(0);
        UpdateLive(0);

        titleScreen.gameObject.SetActive(false);
    }
}
