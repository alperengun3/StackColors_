using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Color playerColor;
    public Color getPlayerColor { get { return playerColor; } }
    [SerializeField] private Renderer[] rends;
    [SerializeField] private bool isPlaying;
    [SerializeField] private float ForwardSpeed;
    [SerializeField] private float SideSpeed;
    Rigidbody rb;
    Transform parentPick;
    [SerializeField] private Transform StackPosition;
    [SerializeField] private float border;
    private bool atEnd;
    [SerializeField] private float forwardForce;
    public float getForwardForce { get { return forwardForce; } }
    [SerializeField] private float forceAdder;
    [SerializeField] private float forceReducer;
    private float a = 0.2f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ColorSet(playerColor);
    }

    void Update()
    {

        if (isPlaying)
        {
            Move();
        }

        if (atEnd)
        {
            forwardForce += forceReducer * Time.deltaTime;
            if (forwardForce<0)
            {
                forwardForce = 0;
            }
        }

    }

    void ColorSet(Color ColorIn)
    {
        playerColor = ColorIn;
        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].material.SetColor("_Color", playerColor);
        }
    }


    void Move()
    {
        float xPosition = Mathf.Clamp(transform.position.x, border * -1f, border * 1f);
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);


        float speed = SideSpeed * Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(1*speed, 0, ForwardSpeed);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ColorWall"))
        {
            ColorSet(other.GetComponent<ColorWall>().GetColor(gameObject));
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLineStart"))
        {
            atEnd = true;
        }
        if (other.CompareTag("FinishLineEnd"))
        {
            rb.velocity = Vector3.zero;
            isPlaying = false;
        }
        if (atEnd)
        {
            return;
        }

        if (other.CompareTag("Collected"))
        {
            Transform otherTransform = other.transform;
            if (playerColor == otherTransform.GetComponent<StackColors>().GetColor())
            {
                Score.getInstance.UpdateScore(otherTransform.GetComponent<StackColors>().getValue);
            }
            else
            {
                Score.getInstance.UpdateScore(otherTransform.GetComponent<StackColors>().getValue);
                Destroy(other.gameObject);
                if (parentPick != null)
                {
                    if (parentPick.childCount > 1)
                    {
                        parentPick.position += Vector3.down * 1 * a;
                        Destroy(parentPick.GetChild(parentPick.childCount - 1).gameObject);

                    }
                    else
                    {
                        Destroy(parentPick.gameObject);
                    }
                }

                return;
            }



            other.enabled = false;
            if (parentPick == null)
            {
                parentPick = otherTransform;
                parentPick.position = StackPosition.position;
                parentPick.parent = StackPosition;
            }
            else
            {
                parentPick.position += Vector3.up * (otherTransform.localScale.y);
                otherTransform.position = StackPosition.position;
                otherTransform.parent = parentPick;
            }
        }
    }

}
