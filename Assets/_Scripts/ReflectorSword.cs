using UnityEngine;
using System.Collections;

public class ReflectorSword : MonoBehaviour
{
    public static ReflectorSword S;
    public Transform t;

    void Awake()
    {
        t = GetComponent<Transform>();
        S = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "faceBullet")
        {
            print("hey");
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().AddRelativeForce(0, 0, -10, ForceMode.Impulse);
        }
    }

}
