using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TreeFaceDialogue : MonoBehaviour
{

    public string[] dia;
    public bool playerHealthLow = false;
    public GameObject player;
    public float wiggleVar;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dia = new string[2];
        dia[0] = "THERE IS NO SHELTER IN HERE FOR YOU";
        dia[1] = "GO AWAY!";
    }

    void OnMouseEnter()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 12)
        {
            if (!RPGTextTyper.S.isCurrentlyPrinting)
            {

                if (HealthBar.S.hp < 25)
                {
                    StartCoroutine(wiggle());
                    dia = new string[5];
                    dia[0] = "HMM... LOOKS LIKE YOU REALLY NEED HELP";
                    dia[1] = "COME IN AND PICK A SWORD...";
                    dia[2] = "THEN";
                    dia[3] = "LEAVE";
                    dia[4] = "FOREVER...";
                    RPGTextTyper.S.printThing(dia, 0.05f, 1.0f);
                    RPGTextTyper.S.isCurrentlyPrinting = true;
                }
                else
                {
                    StartCoroutine(wiggle());
                    RPGTextTyper.S.printThing(dia, 0.05f, 1.0f);
                    RPGTextTyper.S.isCurrentlyPrinting = true;
                }
            }
        }
    }

    IEnumerator wiggle()
    {
        for (int i = 0; i < 12; i++)
        {
            transform.Rotate(0, 0, wiggleVar * Time.deltaTime);
            yield return new WaitForSeconds(0.25f);
            transform.Rotate(0, 0, -wiggleVar * Time.deltaTime);
            yield return new WaitForSeconds(0.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
