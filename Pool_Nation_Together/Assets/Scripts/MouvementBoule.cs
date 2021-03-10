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

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        posInitial = this.transform.position;
    }

    void Update()
    {
        if (!peutBouger)
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
        else
        {
            this.transform.localPosition += new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * vitesseDeplacement, 0, Input.GetAxis("Vertical") * Time.deltaTime * vitesseDeplacement);

            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                peutBouger = false;
                RB.useGravity = true;
            }
        }
    }

    public void DeplacementBoule()
    {
        RB.useGravity = false;
        this.transform.position = new Vector3(posInitial.x, posInitial.y + hauteurQuandDeplacement, posInitial.z);
        peutBouger = true;
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
