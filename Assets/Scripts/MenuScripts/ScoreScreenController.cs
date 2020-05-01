using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScreenController : MonoBehaviour
{
    public TMP_Text ScoreText;

    public void SetScore(float score)
    {
        ScoreText.text = "Your score was: " + score;
    }
}
