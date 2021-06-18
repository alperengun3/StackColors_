using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWall : MonoBehaviour
{
    [SerializeField] private Color newColor;
    void Start()
    {
        Color tempColor = newColor;
        tempColor.a = 0.5f;
        Renderer rend = transform.GetChild(0).GetComponent<Renderer>();
        rend.material.SetColor("_Color", tempColor);
    }

    public Color GetColor(GameObject player)
    {
        for (int i = 0; i < player.transform.GetChild(2).transform.GetChild(0).transform.childCount; i++)
        {
            player.transform.GetChild(2).transform.GetChild(0).transform.GetChild(i).GetComponent<MeshRenderer>().material.color = newColor;
            player.transform.GetChild(2).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = newColor;
        }
        return newColor;
    }
}
