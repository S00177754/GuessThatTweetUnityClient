using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTrialMenuController : MonoBehaviour
{
    public TMP_InputField PokemonNameInput;
    public GameObject gameManagerObj;
    private GameManager gameManager;
    private PokemonAPIQuestionGenerator questionGen;

    private void Start()
    {
        questionGen = gameManager.GetComponent<PokemonAPIQuestionGenerator>();
        gameManager = gameManager.GetComponent<GameManager>();
    }


    public void StartGame()
    {
        questionGen.GeneratePokemonQuestions(PokemonNameInput.text);
        if(questionGen.Questions.Count > 0)
        {
            gameManager.SetGameState(GameState.Playing);
        }
    }
}
