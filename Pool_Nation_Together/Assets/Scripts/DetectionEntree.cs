using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionEntree : MonoBehaviour
{
    private MouvementBoule ScriptBouleblc;

    private void Start()
    {
        ScriptBouleblc = GameObject.FindObjectOfType<MouvementBoule>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boule blanche")
        {
            ScriptBouleblc.DeplacementBoule();
        }
        if (other.tag == "Rayées" || other.tag == "Pleines")
        {
            other.attachedRigidbody.velocity = Vector3.zero;
            Destroy(other.gameObject, 1f);
            ScriptBouleblc.Boules.Remove(other.transform);
        }

    }
}
