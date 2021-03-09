using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionEntree : MonoBehaviour
{
    public Component[] Boules = new Component[15];

    private void Start()
    {
        Boules = GameObject.Find("Set_Boules").GetComponentsInChildren<Component>();
        foreach (Component boule in Boules)
        {
            Debug.Log(boule.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " " + other.tag);
    }
}
