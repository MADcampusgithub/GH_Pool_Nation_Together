using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementBoule : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 15f)]
    float force;
    public float maximumForce;
    [SerializeField]
    [Range(0f, 10f)]
    float sensibiliteForce;
    Vector3 posInitial;
    Rigidbody RB;
    bool peutBouger = false;
    [SerializeField]
    [Range(1f, 200f)]
    float vitesseDeplacement;
    [SerializeField]
    [Range(0f, 10f)]
    float hauteurQuandDeplacement;
    [SerializeField]
    [Range(0.01f, 5f)]
    float minimumSpeed;
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
        if (RB.velocity.magnitude <= minimumSpeed || Input.GetMouseButtonUp(0))
        {
            RB.velocity = Vector3.zero;
        }

        if (!peutBouger)
        {
            Tire();
        }
        else
        {
            RB.AddForce(new Vector3(Input.GetAxis("Horizontal") * vitesseDeplacement * Time.deltaTime * 300, 0, Input.GetAxis("Vertical") * vitesseDeplacement * Time.deltaTime * 300), ForceMode.Acceleration);

            if (Input.GetMouseButtonDown(0))
            {
                this.transform.position = new Vector3(this.transform.position.x, posInitial.y, this.transform.position.z);
                peutBouger = false;
                RB.useGravity = true;
                RB.constraints = RigidbodyConstraints.None;
            }
        }
    }

    public void DeplacementBoule()
    {
        this.transform.position = new Vector3(posInitial.x, posInitial.y + hauteurQuandDeplacement, posInitial.z);
        peutBouger = true;
        RB.useGravity = false;
        RB.constraints = RigidbodyConstraints.FreezePositionY;
        RB.velocity = Vector3.zero;
    }

    private void Tire()
    {
        if (Input.GetMouseButton(0) && RB.velocity == Vector3.zero) //modifie la force lorsqu'on maintient la souris
        {
            force += Input.GetAxis("Mouse Y") * sensibiliteForce;
            if (force < 0f)
            {
                force = 0f;
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
            if (force != 0f)
            {
                RB.AddForce(this.transform.forward * force, ForceMode.Impulse);
                force = 0f;
            }
        }
    }
}
