using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    public float multiplierValue;
    public Color multiplierColor;
    public Renderer[] myRend;
    void Start()
    {
        SetColor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Collected"))
        {
            Score.getInstance.UpdateMultiplier(multiplierValue);
        }
    }

    void SetColor()
    {
        for (int i = 0; i < myRend.Length; i++)
        {
            myRend[i].material.SetColor("_Color", multiplierColor);
        }
    }
}
