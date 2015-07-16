using UnityEngine;
using System.Collections;

public class FaceBullet : MonoBehaviour
{

    Rigidbody rb;
    public bool wasReflected = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(0, 0, 15, ForceMode.Impulse); // 20 originally
        Destroy(gameObject, 6.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 100 * Time.deltaTime);
    }

}
