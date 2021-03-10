using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionEntree : MonoBehaviour
{
    public Component[] Boules = new Component[15];
    private MouvementBoule ScriptBouleblc;

    private void Start()
    {
        Boules = GameObject.Find("Set_Boules").GetComponentsInChildren<Component>();
        foreach (Component boule in Boules)
        {
            Debug.Log(boule.name);
        }
        ScriptBouleblc = GameObject.FindObjectOfType<MouvementBoule>();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " " + other.tag);
        other.attachedRigidbody.velocity = Vector3.zero;
        if (other.tag == "Boule blanche")
        {
            ScriptBouleblc.DeplacementBoule();
        }
    }
}
