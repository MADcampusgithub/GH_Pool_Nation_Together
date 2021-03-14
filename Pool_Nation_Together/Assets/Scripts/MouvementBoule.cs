using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementBoule : MonoBehaviour
{
    [SerializeField][Range(0.1f, 12f)] float force = 0.1f;
    private float minimumForce = 0.1f;
    private float maximumForce = 12f;
    [SerializeField][Range(0f, 10f)] float sensibiliteForce;
    Vector3 posInitial;
    Rigidbody RB;
    public bool peutBouger = false;
    public bool peutTirer = false;

    [SerializeField][Range(1f, 200f)] float vitesseDeplacement;
    [SerializeField][Range(0f, 10f)] float hauteurQuandDeplacement;
    [SerializeField][Range(0.01f, 5f)] float minimumSpeed;
    public List<Transform> Boules = new List<Transform>();

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        posInitial = this.transform.position;

        foreach (Transform boule in GameObject.Find("Set_Boules").GetComponentsInChildren<Transform>())
        {
            if (boule.gameObject.name != "Set_Boules")
            {
                Boules.Add(boule);
            }
        }
    }

    void Update()
    {
        if (!peutBouger)
        {
            Tire();

            if (RB.velocity.magnitude <= minimumSpeed || Input.GetMouseButtonUp(0))
            {
                RB.velocity = Vector3.zero;
                foreach (Transform boule in Boules)
                {
                    if (boule.gameObject.GetComponent<Rigidbody>().velocity.magnitude <= minimumSpeed)
                    {
                        
                    }
                }
            }

        }
        else
        {
            Vector3 mouvement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            RB.AddRelativeForce(mouvement * vitesseDeplacement * Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
            {
                DeplacementBoule(false); 
            }
        }
    }

    public void DeplacementBoule(bool peutDeplacer)
    {
        if (peutDeplacer)
        {
            this.transform.position = new Vector3(posInitial.x, posInitial.y + hauteurQuandDeplacement, posInitial.z);
            peutBouger = true;
            RB.useGravity = false;
            RB.drag = 5f;
            RB.constraints = RigidbodyConstraints.FreezePositionY;
            RB.velocity = Vector3.zero;
            foreach (Transform boule in Boules)
            {
                boule.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, posInitial.y, this.transform.position.z);
            peutBouger = false;
            RB.useGravity = true;
            RB.drag = 0.05f;
            RB.constraints = RigidbodyConstraints.None;
            foreach (Transform boule in Boules)
            {
                boule.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }

    private void Tire()
    {
        if (Input.GetMouseButton(0) && RB.velocity == Vector3.zero) //modifie la force lorsqu'on maintient la souris
        {
            force += Input.GetAxis("Mouse Y") * sensibiliteForce;
            if (force < minimumForce)
            {
                force = minimumForce;
            }
            else
            {
                if (force > maximumForce)
                {
                    force = maximumForce;
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) //tire lorsqu'on relanche la souris
        {
            if (force != minimumForce)
            {
                RB.AddForce(this.transform.forward * force, ForceMode.Impulse);
                force = 0.1f;
            }
        }
    }
}
