using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum QuestionGroup { MCQ, PokedexNum, FillInBlank, ScoreDisplay}

[RequireComponent(typeof(PokemonAPIQuestionGenerator))]
public class QuestionsController : MonoBehaviour
{
    public GameObject MCQQuestion;
    public GameObject PokedexNumberQuestion;
    public GameObject FillInTheBlankQuestion;
    public GameObject ScoreDisplay;
    public GameObject ImageDisplay;
    public TMP_Text TimerDisplay;

    public Image spriteToDisplay;
    public PokemonAPIQuestionGenerator questionGen;

    public int QuestionCount = 5;
    private float Timer = 300f;
    private float LastQuestionAnsweredAt = 0f;
    private bool TimerActive = false;

    private void Start()
    {
      
    }

    private void Update()
    {
        if (TimerActive)
        {
            Timer -= Time.deltaTime * 1;
            TimerDisplay.text = "Time Left: " + Math.Round(Timer,2).ToString();
        }
    }

    public void SetActiveQuestionGroup(QuestionGroup group)
    {
        switch (group)
        {
            case QuestionGroup.MCQ:
                MCQQuestion.SetActive(true);
                PokedexNumberQuestion.SetActive(false);
                FillInTheBlankQuestion.SetActive(false);
                ScoreDisplay.SetActive(false);
                break;

            case QuestionGroup.PokedexNum:
                MCQQuestion.SetActive(false);
                PokedexNumberQuestion.SetActive(true);
                FillInTheBlankQuestion.SetActive(false);
                ScoreDisplay.SetActive(false);
                break;

            case QuestionGroup.FillInBlank:
                MCQQuestion.SetActive(false);
                PokedexNumberQuestion.SetActive(false);
                ScoreDisplay.SetActive(false);
                FillInTheBlankQuestion.SetActive(true);
                break;

            case QuestionGroup.ScoreDisplay:
                MCQQuestion.SetActive(false);
                PokedexNumberQuestion.SetActive(false);
                FillInTheBlankQuestion.SetActive(false);
                ScoreDisplay.SetActive(true);
                break;
        }
    }

    public void Activate()
    {
        ImageDisplay.SetActive(true);
        TimerDisplay.gameObject.SetActive(true);
    }

    public void DeActivate()
    {
        MCQQuestion.SetActive(false);
        FillInTheBlankQuestion.SetActive(false);
        PokedexNumberQuestion.SetActive(false);
        ImageDisplay.SetActive(false);
        TimerDisplay.gameObject.SetActive(false);
        ScoreDisplay.SetActive(false);
    }

    public void SetSprite(int pokedexNum)
    {
        StartCoroutine(DownloadSprite(pokedexNum));
    }

    IEnumerator DownloadSprite(int pokedexNum)
    {
        string url = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/" + pokedexNum + ".png";
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
        DownloadHandler handler = webRequest.downloadHandler;

        yield return webRequest.SendWebRequest();

        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            UnityEngine.Debug.Log("Error while Receiving: " + webRequest.error);
        }
        else
        {
            UnityEngine.Debug.Log("Success");

            //Load Image
            Texture2D texture2d = DownloadHandlerTexture.GetContent(webRequest);

            Sprite sprite = null;
            sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), Vector2.zero);

            if (sprite != null)
            {
                spriteToDisplay.sprite = sprite;
            }
        }
    }

    public float TimerLap()
    {
        LastQuestionAnsweredAt = Timer;
        float questionElapsed = LastQuestionAnsweredAt - Timer;
        return questionElapsed;
    }

    public void EndGame()
    {
        TimerActive = false;
        SetActiveQuestionGroup(QuestionGroup.ScoreDisplay);
        ScoreDisplay.GetComponent<ScoreScreenController>().SetScore(GetTotalScore());

        //Post result
        ASPNetAPIHelper.PostRecord(new GameRecordDTO()
        {
            PlayerUsername = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().Username,
            Score = GetTotalScore(),
            PokemonName = questionGen.Questions.First().PokemonName,
            Date = DateTime.Now,
            CompletionTime = LastQuestionAnsweredAt - Timer,
       

        }) ;
    }

    private int GetTotalScore()
    {
        int total = 0;
        foreach (var item in questionGen.Questions)
        {
            total += item.Score;
        }
        return total;
    }

    public void NextQuestion()
    {
        
        if (QuestionCount > 0)
        {
            TimerActive = true;
            PokemonQuestion question = questionGen.GetNextQuestion();
            QuestionCount--;

            switch (question.QuestionType)
            {
                case QuestionType.Name:
                case QuestionType.FlavorText:
                    FillInTheBlankQuestion.GetComponent<FillTextController>().SetQuestion(question);
                    SetActiveQuestionGroup(QuestionGroup.FillInBlank);
                    break;

                case QuestionType.PokedexNumber:
                    PokedexNumberQuestion.GetComponent<PNQController>().SetQuestion(question);
                    SetActiveQuestionGroup(QuestionGroup.PokedexNum);
                    break;


                case QuestionType.EggGroup:
                case QuestionType.Generation:
                    MCQQuestion.GetComponent<MCQController>().SetQuestion(question);
                    SetActiveQuestionGroup(QuestionGroup.MCQ);
                    break;


            }

        }
        else
        {
            EndGame();
        }
    }
}
