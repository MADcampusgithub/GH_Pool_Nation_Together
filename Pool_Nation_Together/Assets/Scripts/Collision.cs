using System.Collections;
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
        if ((Time.time - startTimer) >= 1f && bouleEntree && ScriptBoule.BoulesArrete())
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
        if (other.gameObject.tag == "Rayées" || other.gameObject.tag == "Pleines")
        {
            ScriptBoule.Boules.Remove(other.transform);
            if (this.gameObject.tag == "Entree")
            {
                Destroy(other.gameObject);
            }
        }

        string message = "";
        foreach (Transform boule in ScriptBoule.Boules)
        {
            message += boule.name + " + " + boule.tag + "\n";
        }
        Debug.Log(message);
    }
}
