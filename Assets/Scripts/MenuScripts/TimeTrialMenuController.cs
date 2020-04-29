using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTrialMenuController : MonoBehaviour
{
    public TMP_InputField PokemonNameInput;
    public PokemonAPIQuestionGenerator questionGen;


    public void StartGame()
    {
        questionGen.GeneratePokemonQuestions(PokemonNameInput.text);
    }
}
