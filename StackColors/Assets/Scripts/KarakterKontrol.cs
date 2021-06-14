using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterKontrol : MonoBehaviour
{
    public Color Color1;
    public Renderer[] rends;
    public bool isPlaying;
    public float ForwardSpeed;
    public float SideSpeed;
    Rigidbody rb;
    Transform parentPick;
    public Transform StackPosition;
    public float border;
    bool atEnd;
    public float forwardForce;
    public float forceAdder;
    public float forceReducer;
    public static Action<float> kick;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ColorSet(Color1);
    }

    void Update()
    {

        if (isPlaying)
        {
            ForwardMove();
        }

        if (atEnd)
        {
            forwardForce += forceReducer * Time.deltaTime;
            if (forwardForce<0)
            {
                forwardForce = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (atEnd)
            {
                forwardForce += forceAdder;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (atEnd)
            {
                return;
            }

            if (isPlaying == false)
            {
                isPlaying = true;
            }
            
        }

        Move();
    }

    void ColorSet(Color ColorIn)
    {
        Color1 = ColorIn;
        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].material.SetColor("_Color", Color1);
        }
    }

    void ForwardMove()
    {
        rb.velocity = Vector3.forward * ForwardSpeed;
    }

    void Move()
    {
        float xPosition = Mathf.Clamp(transform.position.x, border * -1f, border * 1f);
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);


        float hiz = SideSpeed * Input.GetAxis("Horizontal");
        transform.Translate(hiz * Time.deltaTime, 0, 0);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ColorWall")
        {
            ColorSet(other.GetComponent<ColorWall>().GetColor());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLineStart")
        {
            atEnd = true;
        }
        if (other.tag == "FinishLineEnd")
        {
            rb.velocity = Vector3.zero;
            isPlaying = false;
            LaunchStack();
        }
        if (atEnd)
        {
            return;
        }

        if (other.tag == "Toplanan")
        {
            Transform otherTransform = other.transform;
            if (Color1 == otherTransform.GetComponent<StackColors>().GetColor())
            {
                Score.instance.UpdateScore(otherTransform.GetComponent<StackColors>().value);
            }
            else
            {
                Score.instance.UpdateScore(otherTransform.GetComponent<StackColors>().value);
                Destroy(other.gameObject);
                if (parentPick != null)
                {
                    if (parentPick.childCount > 1)
                    {
                        parentPick.position -= Vector3.up * parentPick.GetChild(parentPick.childCount - 1).localScale.y;
                        Destroy(parentPick.GetChild(parentPick.childCount - 1).gameObject);
                    }
                    else
                    {
                        Destroy(parentPick.gameObject);
                    }
                }

                return;
            }



            Rigidbody otherRB = otherTransform.GetComponent<Rigidbody>();
            otherRB.isKinematic = true;
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

    void LaunchStack()
    {
        kick(forwardForce);
    }

}
