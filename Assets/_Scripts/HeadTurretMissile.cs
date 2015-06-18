using UnityEngine;
using System.Collections;

public class HeadTurretMissile : MonoBehaviour
{

    public float shootForce;
    public float rotateSpeed;
    public float smoothLookAtSpeed;
    public float homingDelay;
    public float homingSpeed;
    public bool isHoming = false;

    Rigidbody rb;
    ParticleSystem ps;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ps = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    // Use this for initialization
    void Start()
    {
        rb.AddRelativeForce(0, 0, shootForce, ForceMode.Impulse);

        StartCoroutine(waitAndHome());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        if (isHoming)
        {
            //transform.LookAt(GameObject.Find("1stP_Player").transform.position);
            SmoothLookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
            rb.velocity = Vector3.zero;
            rb.AddRelativeForce(0, 0, homingSpeed, ForceMode.VelocityChange);
        }

        if (!ps.IsAlive())
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "sword")
            rb.AddRelativeForce(0, 0, -20, ForceMode.Impulse);

        isHoming = false;
        //GetComponent<Collider>().enabled = false;
        //GetComponent<MeshRenderer>().enabled = false;
        //Destroy(rb);
        ps.Stop();
    }

    IEnumerator waitAndHome()
    {
        yield return new WaitForSeconds(homingDelay);
        isHoming = true;
    }

    void SmoothLookAt(Vector3 newDirection)
    {
        newDirection -= this.transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDirection), smoothLookAtSpeed * Time.deltaTime);
    }

}