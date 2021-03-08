using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementBoule : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 1000f)]
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
            RD.AddForce(Vector3.forward * force, ForceMode.Impulse);

        }
    }
}
