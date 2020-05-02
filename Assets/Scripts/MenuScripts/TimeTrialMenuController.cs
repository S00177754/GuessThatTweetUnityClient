using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTrialMenuController : MonoBehaviour
{
    public TMP_InputField PokemonNameInput;
    public PokemonAPIQuestionGenerator questionGen;
    public bool RandomGame = false;

    public void StartGame()
    {
        if (!RandomGame)
        {
            questionGen.GeneratePokemonQuestions(PokemonNameInput.text);
        }
        else
        {
            questionGen.GeneratePokemonQuestions(Random.Range(1, PokemonAPIHelper.MaxRange).ToString());
        }
    }

}
