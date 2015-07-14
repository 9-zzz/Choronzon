using UnityEngine;
using System.Collections;

public class ReflectorSword : MonoBehaviour
{
    public Transform t;
    public static ReflectorSword S;
    public ParticleSystem swordClangFX;

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
            if (Melee.S.isSlashing)
            {
                other.gameObject.GetComponent<TrailRenderer>().enabled = true;
                //print("hey");
                var clangFX = Instantiate(swordClangFX, other.transform.position, other.transform.rotation) as GameObject;
                Destroy(clangFX, 1.0f);
                other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.GetComponent<Rigidbody>().AddRelativeForce(0, 0, -20, ForceMode.Impulse); // Was 10
            }
        }
    }

}
