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

    public GameObject brokenMissile;

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
        //transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
        // Can't do rotating missile because of how I do SmoothLookAt.

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

    IEnumerator FadeTo(float aValue, float aTime, GameObject objectToFade)
    {
        float alpha = objectToFade.GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            objectToFade.GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        var pieces = Instantiate(brokenMissile, transform.position, transform.rotation) as GameObject;
        pieces.transform.GetChild(0).GetComponent<Rigidbody>().AddExplosionForce(50, pieces.transform.position, 200.0f, 0.0F, ForceMode.Impulse);
        pieces.transform.GetChild(1).GetComponent<Rigidbody>().AddExplosionForce(5, pieces.transform.position, 200.0f, 0.0F, ForceMode.Impulse);
        pieces.transform.GetChild(2).GetComponent<Rigidbody>().AddExplosionForce(20, pieces.transform.position, 200.0f, 0.0F, ForceMode.Impulse);

        //StartCoroutine(FadeTo(0.0f, 2.25f, pieces.transform.GetChild(0).gameObject));

        Destroy(pieces, 7.0f);

        if (other.gameObject.tag == "sword")
        {
            rb.AddRelativeForce(0, 0, -20, ForceMode.Impulse);
        }

        isHoming = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        ps.playbackSpeed = 2;
        ps.Stop();

        if(!ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }

    IEnumerator waitAndHome()
    {
        yield return new WaitForSeconds(homingDelay);
        isHoming = true;
    }

    void SmoothLookAt(Vector3 pointToLookAt)
    {
        Vector3 newDirection = pointToLookAt - this.transform.position; // Get the direction to look in.
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newDirection), smoothLookAtSpeed * Time.deltaTime);
        // Use Quaternion.RotateTowards for consistent results
        // For handling a desired acceleration/approach model Unity provides 
        //Mathf.SmoothDamp which produces far more consistent results than that kind of critically damped lerp (Also, use Slerp for rotations?)
    }

}