using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSortie : MonoBehaviour
{
    private MouvementBoule ScriptBouleblc;
    private float startTimer;
    private bool bouleSortie = false;

    private void Start()
    {
        ScriptBouleblc = GameObject.FindObjectOfType<MouvementBoule>();
    }
    private void Update()
    {
        if (bouleSortie)
        {
            if ((Time.time - startTimer) >= 2f)
            {
                ScriptBouleblc.DeplacementBoule();
                bouleSortie = false;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boule blanche")
        {
            startTimer = Time.time;
            bouleSortie = true;            
        }
        if (other.tag == "Rayées" || other.tag == "Pleines")
        {
            other.enabled = false;
            ScriptBouleblc.Boules.Remove(other.transform);
        }

    }
}
