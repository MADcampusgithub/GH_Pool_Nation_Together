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
    Rigidbody RB;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    void Update()
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
