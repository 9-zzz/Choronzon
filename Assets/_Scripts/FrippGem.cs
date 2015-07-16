using UnityEngine;
using System.Collections;

public class FrippGem : MonoBehaviour
{

    Rigidbody rb;
    public bool collidedWithGround = false;
    Vector3 collisionPoint;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(0, 12, 0, ForceMode.Impulse);
        GetComponent<Collider>().enabled = false;
        StartCoroutine(colliderFix());
    }

    // Update is called once per frame
    void Update()
    {

    }
    
     IEnumerator colliderFix()
    {
        yield return new WaitForSeconds(1.0f);
            GetComponent<Collider>().enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("terrain"))
        {
            collidedWithGround = true;
            rb.velocity = Vector3.zero;
            rb.AddRelativeForce(0, 8, 0, ForceMode.Impulse);
        }
    }

}
