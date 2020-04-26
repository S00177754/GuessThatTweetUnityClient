using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum QuestionGroup { MCQ, PokedexNum, FillInBlank}

[RequireComponent(typeof(PokemonAPIQuestionGenerator))]
public class QuestionsController : MonoBehaviour
{
    public GameObject MCQQuestion;
    public GameObject PokedexNumberQuestion;
    public GameObject FillInTheBlankQuestion;

    public Image spriteToDisplay;
    private PokemonAPIQuestionGenerator questionGen;

    public int QuestionCount = 5;
    private float Timer = 0f;
    private float LastQuestionAnsweredAt = 0f;
    private bool TimerActive = false;

    private void Start()
    {
        questionGen = GetComponent<PokemonAPIQuestionGenerator>();
    }

    private void Update()
    {
        if (TimerActive)
        {
            Timer += Time.deltaTime * 1;
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
                break;

            case QuestionGroup.PokedexNum:
                MCQQuestion.SetActive(false);
                PokedexNumberQuestion.SetActive(true);
                FillInTheBlankQuestion.SetActive(false);
                break;

            case QuestionGroup.FillInBlank:
                MCQQuestion.SetActive(false);
                PokedexNumberQuestion.SetActive(false);
                FillInTheBlankQuestion.SetActive(true);
                break;
        }
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
        float questionElapsed = Timer - LastQuestionAnsweredAt;
        LastQuestionAnsweredAt = Timer;
        return questionElapsed;
    }

    public void EndGame()
    {

    }

    public void NextQuestion()
    {
        if (QuestionCount > 0)
        {
            PokemonQuestion question = questionGen.GetNextQuestion();
            QuestionCount--;

            switch (question.QuestionType)
            {
                case QuestionType.Name:
                case QuestionType.FlavorText:
                    //MCQQuestion.GetComponent<FillTextController>().SetQuestion(question);
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
