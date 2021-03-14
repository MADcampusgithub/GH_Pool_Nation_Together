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
    string[] difficultes = new string[] { "Champion", "Difficile", "Moyen", "Facile" };
    public string difficulte;

    [SerializeField][Range(1f, 200f)] float vitesseDeplacement;
    [SerializeField][Range(0f, 10f)] float hauteurQuandDeplacement;
    [SerializeField][Range(0.01f, 5f)] float minimumSpeed;
    public List<Transform> Boules = new List<Transform>();
    public float vitesseRalentissement;
    [SerializeField]
    [Range(-0.3f, 0.3f)] public float decalage = 0f;

    void Start()
    {
        difficulte = difficultes[3];
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
                RB.velocity = Vector3.Lerp(RB.velocity, Vector3.zero, vitesseRalentissement);
                foreach (Transform boule in Boules)
                {
                    if (boule.gameObject.GetComponent<Rigidbody>().velocity.magnitude <= minimumSpeed)
                    {
                        boule.gameObject.GetComponent<Rigidbody>().velocity = Vector3.Lerp(boule.gameObject.GetComponent<Rigidbody>().velocity, Vector3.zero, vitesseRalentissement);
                    }
                }
                if (BoulesArrete())
                {
                    foreach (Transform boule in Boules)
                    {
                        boule.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
        if (Arrondi(RB.velocity.magnitude, 2) == 0f) //modifie la force lorsqu'on maintient la souris
        {
            RB.velocity = Vector3.zero;
            RB.angularVelocity = Vector3.zero;
            Prediction("Facile", 0.01f);

            if (Input.GetMouseButton(0))
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

    private float Arrondi(float a, int n)
    {
        float resultat;
        resultat = Mathf.Round(a * Mathf.Pow(10, n));
        resultat /= Mathf.Pow(10, n);
        return resultat;
    }

    public bool BoulesArrete()
    {
        foreach (Transform boule in Boules)
        {
            if (Arrondi(boule.gameObject.GetComponent<Rigidbody>().velocity.magnitude, 2) != 0)
            {
                return false;
            }
        }
        return true;
    }

    private void Prediction(string difficulte, float lineOffset)
    {
        //champion, difficile, moyen, facile
        float moitierTaille = this.transform.localScale.x / 2;

        Vector3 direction = this.transform.forward;

        RaycastHit hitMin;
        Physics.Raycast(this.transform.position, this.transform.position, out hitMin);
        Vector3 originMin = Vector3.zero;
        float decalageMin;

        if (difficulte == difficultes[1] || difficulte == difficultes[2] || difficulte == difficultes[3])
        {
            float minDist = 30f;

            for (decalage = -moitierTaille; decalage <= moitierTaille; decalage += 0.005f)
            {
                float avancer = -Mathf.Abs(decalage) + moitierTaille;
                Vector3 origin = this.transform.position + this.transform.right * decalage + this.transform.forward * avancer;

                RaycastHit hit;
                Physics.Raycast(origin, direction, out hit);

                if (Vector3.Distance(origin, hit.point) < minDist)
                {
                    minDist = Vector3.Distance(origin, hit.point);
                    Physics.Raycast(origin, direction, out hitMin);
                    originMin = origin;
                    decalageMin = decalage;
                }
            }
            Debug.DrawLine(originMin, hitMin.point);
        }

        if (difficulte == difficultes[2] || difficulte == difficultes[3])
        {
            Vector3 origin = hitMin.point;            
        }

    }
}
