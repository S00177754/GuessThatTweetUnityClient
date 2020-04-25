using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

    public void NextQuestion()
    {
        PokemonQuestion question = questionGen.GetNextQuestion();
        QuestionCount--;

        switch (question.QuestionType)
        {
            case QuestionType.Name:
            case QuestionType.FlavorText:
                //MCQQuestion.GetComponent<FillTextController>().SetQuestion(question);
                break;

            case QuestionType.PokedexNumber:
                //MCQQuestion.GetComponent<PNQController>().SetQuestion(question);
                break;


            case QuestionType.EggGroup:
            case QuestionType.Generation:
                MCQQuestion.GetComponent<MCQController>().SetQuestion(question);
                break;

           
        }
    }
}
