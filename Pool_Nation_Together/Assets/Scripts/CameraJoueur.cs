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
        if (RB_boule.velocity.magnitude <= minimumSpeed || Input.GetMouseButtonUp(0))
        {
            RB_boule.velocity = Vector3.zero;
        }
        if (RB_boule.velocity.magnitude == 0f && !Input.GetMouseButton(0))
        {
            RB_boule.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, 0);
            CamPlacement();
        }
    }

    private void CamPlacement()
    {
        if (Arrondi(Vector3.Distance(RB_boule.transform.position, this.transform.position), 2) != Arrondi(offset.magnitude, 2))
        {
            this.transform.position = RB_boule.transform.position + offset;
            this.transform.LookAt(RB_boule.transform);
        }
        else
        {
            this.transform.RotateAround(RB_boule.transform.position, new Vector3(0, 1, 0), Input.GetAxis("Mouse X") * sensibilite * Time.deltaTime * 100);
        }
    }
    private float Arrondi(float f, int n)
    {
        float resultat;
        resultat = Mathf.Round(f * Mathf.Pow(10, n));
        resultat /= Mathf.Pow(10, n);
        return resultat;
    }
}
