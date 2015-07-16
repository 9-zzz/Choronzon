using UnityEngine;
using System.Collections;

public class HealthPickUp : MonoBehaviour
{

    GameObject Player;
    public float distance;
    public bool foundPlayer = false;
    public int randInt;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        randInt = Random.Range(0, 4);

        Player = GameObject.Find("CPlayer");
        StartCoroutine(waitAndFindPlayer());

        if (randInt == 0)
            addForce1();
        if (randInt == 1)
            addForce2();
        if (randInt == 2)
            addForce3();
        if (randInt == 3)
            addForce4();
    }

    void addForce1() { rb.AddForce(2, 0, 0, ForceMode.Impulse); }
    void addForce2() { rb.AddForce(0, 2, 0, ForceMode.Impulse); }
    void addForce3() { rb.AddForce(0, 0, 2, ForceMode.Impulse); }
    void addForce4() { rb.AddForce(-2, 0, 0, ForceMode.Impulse); }

    IEnumerator waitAndFindPlayer()
    {
        yield return new WaitForSeconds(2.0f);
        foundPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, Player.transform.position);

        //if (distance < 20) { }

        if (foundPlayer)
        {
            transform.LookAt(Player.transform.position);
            GetComponent<Rigidbody>().AddRelativeForce(0, 0, 100);
        }
    }

}
