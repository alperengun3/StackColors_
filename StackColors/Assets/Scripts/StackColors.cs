using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackColors : MonoBehaviour
{
    public int value;
    public Color StackColor;
    public Rigidbody pickUpRB;
    public Collider pickUpCollider;

    private void OnEnable()
    {
        KarakterKontrol.kick += Mykick;
    }

    private void OnDisable()
    {
        KarakterKontrol.kick += Mykick;
    }

    private void Mykick(float forceSent)
    {
        transform.parent = null;
        pickUpCollider.enabled = true;
        pickUpRB.isKinematic = false;
        pickUpRB.AddForce(new Vector3(0, forceSent, forceSent));
    }
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", StackColor);
    }

    public Color GetColor()
    {
        return StackColor;
    }
}
