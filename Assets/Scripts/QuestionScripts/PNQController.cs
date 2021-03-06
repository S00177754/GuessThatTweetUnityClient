﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PNQController : MonoBehaviour
{
    private PokemonQuestion PokeQuestion;
    public TMP_InputField InputField;
    public TMP_Text QuestionText;
    public QuestionsController questionController;

    public void SetQuestion(PokemonQuestion question)
    {
        PokeQuestion = question;
        QuestionText.text = PokeQuestion.Question;
    }

    public void SubmitAnswer()
    {
        if(int.Parse(PokeQuestion.Answer.ToLower()) == int.Parse(InputField.text.ToLower()))
        {
            PokeQuestion.CalculateScore(questionController.TimerLap());
        }
        else
        {
            PokeQuestion.Score = 0;
        }

        InputField.text = "";

        questionController.NextQuestion();
    }
}
