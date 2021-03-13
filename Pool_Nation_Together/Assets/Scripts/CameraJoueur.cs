using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJoueur : MonoBehaviour
{
    MouvementBoule ScriptBoule;
    public GameObject bouleBlc;
    [SerializeField]
    [Range(0.1f, 10f)]
    float sensibilite;

    void Start()
    {
        ScriptBoule = GameObject.FindObjectOfType<MouvementBoule>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!ScriptBoule.peutBouger)
        {
            if (bouleBlc.GetComponent<Rigidbody>().velocity.magnitude == 0f && !Input.GetMouseButton(0))
            {
                this.transform.position = bouleBlc.transform.position;
                this.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * sensibilite * Time.deltaTime * 100, 0));
                bouleBlc.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, 0);
            }
        }
        else
        {
            this.transform.position = bouleBlc.transform.position;
            this.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * sensibilite * Time.deltaTime * 100, 0));
            bouleBlc.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, 0);
        }
        
    }
}
