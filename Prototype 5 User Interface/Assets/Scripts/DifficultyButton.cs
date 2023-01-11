using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    // Component variables
    private Button button;
    private GameManager gameManager;

    // Basic data type variables
    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        // store components
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        button = GetComponent<Button>();

        // if the button is pressed call the method
        button.onClick.AddListener(SetDifficulty);
    }

    // Custom method to set difficulty
    void SetDifficulty()
    {
        Debug.Log(button.gameObject.name + " was pressed.");

        // starts game
        gameManager.StartGame(difficulty);
    }
}
