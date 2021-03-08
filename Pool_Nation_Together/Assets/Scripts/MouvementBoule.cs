using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementBoule : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 20f )]
    float force;
    Rigidbody RD;

    void Start()
    {
        RD = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RD.AddForce(this.transform.forward * force, ForceMode.Impulse);
        }
    }
}
