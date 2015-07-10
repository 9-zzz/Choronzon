using UnityEngine;
using System.Collections;

public class PointyHands : MonoBehaviour
{

    public GameObject faceBullet;
    public float smoothLookAtSpeed;
    public float distFromPlayer;
    public bool charging = false;
    public Color redEmColor;

    Light spLight;
    GameObject sp; // Spawnpoint
    GameObject player;
    //Rigidbody rb;
    Rigidbody rbParent;

    // Use this for initialization
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        rbParent = transform.parent.GetComponent<Rigidbody>();
        sp = transform.GetChild(0).gameObject;
        spLight = sp.GetComponent<Light>();
        player = GameObject.Find("Player");
        StartCoroutine(moveCycle());
        //redEmColor = GetComponent<Renderer>().materials[0].color;
        this.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        distFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        goSmoothLookAt(transform.parent.gameObject, player.transform.position);
        goSmoothLookAt(sp, player.transform.position);
        //SmoothLookAt(player.transform.position);

        GetComponent<Renderer>().materials[0].SetColor("_EmissionColor", redEmColor);

        if (charging)
        {
            redEmColor = Color.Lerp(redEmColor, Color.red, 2 * Time.deltaTime);
            spLight.intensity = Mathf.Lerp(spLight.intensity, 0.4f, 2 * Time.deltaTime);
        }
        else
        {
            redEmColor = Color.Lerp(redEmColor, Color.black, 1 * Time.deltaTime);
            spLight.intensity = Mathf.Lerp(spLight.intensity, 0, 1.6f * Time.deltaTime);
        }
    }

    IEnumerator moveCycle()
    {
        yield return new WaitForSeconds(2.5f);
        while (true)
        {
            rbParent.AddRelativeForce(0, 0, 2, ForceMode.VelocityChange);
            if (Random.Range(0, 3) == 0)
            {
                rbParent.AddRelativeForce(1, 0, 3, ForceMode.VelocityChange);
            }
            if (distFromPlayer < 20)
            {
                yield return new WaitForSeconds(2.0f);
                rbParent.velocity = Vector3.zero;
                charging = true;
                yield return new WaitForSeconds(0.5f);
                yield return new WaitForSeconds(0.1f);
                FaceShoot();
                yield return new WaitForSeconds(0.1f);
                FaceShoot();
                yield return new WaitForSeconds(0.1f);
                FaceShoot();
                yield return new WaitForSeconds(0.1f);
                FaceShoot();
                yield return new WaitForSeconds(0.1f);
                FaceShoot();
                charging = false;
            }
            yield return new WaitForSeconds(2.0f);
        }
    }

    void FaceShoot()
    {
        Instantiate(faceBullet, sp.transform.position, sp.transform.rotation);
    }

    void goSmoothLookAt(GameObject go, Vector3 pointToLookAt)
    {
        Vector3 newDirection = pointToLookAt - go.transform.position; // Get the direction to look in.
        go.transform.rotation = Quaternion.Slerp(go.transform.rotation, Quaternion.LookRotation(newDirection), (smoothLookAtSpeed * 4.0f) * Time.deltaTime);
    }

    void SmoothLookAt(Vector3 pointToLookAt)
    {
        Vector3 newDirection = pointToLookAt - this.transform.position; // Get the direction to look in.
        newDirection.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newDirection), smoothLookAtSpeed * Time.deltaTime);
        // Use Quaternion.RotateTowards for consistent results
        // For handling a desired acceleration/approach model Unity provides 
        //Mathf.SmoothDamp which produces far more consistent results than that kind of critically damped lerp
    }

}
