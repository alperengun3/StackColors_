using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackColors : MonoBehaviour
{
    
    public int getValue 
    { 
        get { return value; }
    }
    [SerializeField] private int value;
    [SerializeField] private Color stackColor;
    [SerializeField] private Rigidbody pickUpRB;
    [SerializeField] private float kickSpeed;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.AddComponent<BoxCollider>();
        }


        if (other.gameObject.CompareTag("Kick"))
        {

            gameObject.AddComponent<Rigidbody>();

            pickUpRB.AddForce(transform.up * kickSpeed);

        }

    }

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", stackColor);
    }

    public Color GetColor()
    {
        return stackColor;
    }

    

}
