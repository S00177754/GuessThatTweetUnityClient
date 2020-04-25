using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MCQController : MonoBehaviour
{
    private PokemonQuestion Question;
    public List<Button> Buttons = new List<Button>(4);
    public QuestionsController questionController;

    public void SetQuestion(PokemonQuestion question)
    {
        Question = question;
        for (int i = 0; i < question.Choices.Count; i++)
        {
            Buttons[i].GetComponentInChildren<TMP_Text>().text = question.Choices[i];
        }
    }

    public void SelectAnswer()
    {
        if(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text == Question.Answer)
        {
            //correct
        }
        else
        {
            //wrong
        }
    }
}
