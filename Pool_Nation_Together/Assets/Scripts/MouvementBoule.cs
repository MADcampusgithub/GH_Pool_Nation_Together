using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementBoule : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 20f )]
    float force;
    Rigidbody RB;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RB.AddForce(this.transform.forward * force, ForceMode.Impulse);
        }
    }
}
