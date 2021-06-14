using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;
    public Text ScoreDisplay;
    int score;
    float multiplierValue;

    private void OnEnable()
    {
        if (instance==null)
        {
            instance = this;
        }
    }

    public void UpdateScore(int valueIn)
    {
        score += valueIn;
        ScoreDisplay.text = score.ToString();
    }

    public void UpdateMultiplier(float valueIn)
    {
        if (valueIn<=multiplierValue)
        {
            return;
        }
        multiplierValue = valueIn;
        ScoreDisplay.text = (score * multiplierValue).ToString();
    }

}
