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

    public void SetSprite(string url)
    {
        StartCoroutine(DownloadSprite(url));
    }

    IEnumerator DownloadSprite(string url)
    {
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
}
