﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    MouvementBoule ScriptBoule;
    private float startTimer;
    public bool bouleEntree = false;

    void Start()
    {
        ScriptBoule = GameObject.FindObjectOfType<MouvementBoule>();
    }

    void Update()
    {
        if ((Time.time - startTimer) >= 1f && bouleEntree)
        {
            ScriptBoule.DeplacementBoule(true);
            bouleEntree = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boule blanche")
        {
            startTimer = Time.time;
            bouleEntree = true;
        }
        if (other.tag == "Rayées" || other.tag == "Pleines")
        {
            ScriptBoule.Boules.Remove(other.transform);
            Destroy(other, 2f);
        }
    }
}
