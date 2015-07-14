using UnityEngine;
using System.Collections;

public class ReplaceWithUnshattered : MonoBehaviour
{

    public GameObject pointyHands;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<TriggerUnshatter>().isWhole)
        {
            Destroy(GetComponent<SphereCollider>()); // Location shifting... WEIRD 
            Instantiate(pointyHands, transform.position - transform.up * 2.56f, transform.rotation);
            Destroy(gameObject);
        }
    }

}
