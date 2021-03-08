using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJoueur : MonoBehaviour
{
    public Rigidbody RB_boule;
    Vector3 offset;
    [SerializeField]
    [Range(0.01f, 5f)]
    float minimumSpeed;
    [SerializeField]
    [Range(0.1f, 10f)]
    float sensibilite;

    void Start()
    {
        offset = this.transform.position - RB_boule.transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (RB_boule.velocity.magnitude <= minimumSpeed || Input.GetKeyDown(KeyCode.Space))
        {
            CamPlacement();
        }
        if (RB_boule.velocity.magnitude == 0f)
        {
            RB_boule.transform.rotation = this.transform.rotation;
        }
    }

    private void CamPlacement()
    {
        if (Mathf.Floor(Vector3.Distance(RB_boule.transform.position, this.transform.position)) != Mathf.Floor(offset.magnitude))
        {
            this.transform.position = RB_boule.transform.position + offset;

        }
        else
        {
            this.transform.RotateAround(RB_boule.transform.position, new Vector3(0, 1, 0), Input.GetAxis("Mouse X") * sensibilite * Time.deltaTime * 100);
        }
    }
}
