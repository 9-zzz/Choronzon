using UnityEngine;
using System.Collections;

public class PointyHands : MonoBehaviour
{

    public GameObject faceBullet;
    public float smoothLookAtSpeed;
    public float distFromPlayer;
    public bool charging = false;
    public bool powerOn = false;
    public bool hasPoweredOn = false;
    public Color redEmColor;
    public Color originalRedEmColor;

    Light spLight;
    GameObject sp; // Spawnpoint
    GameObject player;
    //Rigidbody rb;
    Rigidbody rbParent;

    void Awake()
    {
        //originalRedEmColor = GetComponent<Renderer>().materials[0].color;
        //GetComponent<Renderer>().materials[0].SetColor("_EmissionColor", redEmColor);
    }

    // Use this for initialization
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        rbParent = transform.parent.GetComponent<Rigidbody>();
        sp = transform.GetChild(0).gameObject;
        spLight = sp.GetComponent<Light>();
        player = GameObject.Find("Player");
        StartCoroutine(moveCycle());
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

        ChargingFX();

        if (hasPoweredOn == false)
            PowerOnFX();
    }

    void PowerOnFX()
    {
        if (powerOn)
        {
            ColorAndLightFX(Color.red, 10.0f, 0.95f, 10.0f);
        }
        else
        {
            ColorAndLightFX(Color.black, 5.0f, 0.0f, 5.0f);
        }
    }

    void ChargingFX()
    {
        if (charging)
        {
            ColorAndLightFX(Color.red, 2.0f, 0.25f, 2.0f);
        }
        else
        {
            ColorAndLightFX(Color.black, 1.0f, 0.0f, 1.6f);
        }
    }

    void ColorAndLightFX(Color targetColor, float cSpeed, float targetIntensity, float lSpeed)
    {
        redEmColor = Color.Lerp(redEmColor, targetColor, cSpeed * Time.deltaTime);
        spLight.intensity = Mathf.Lerp(spLight.intensity, targetIntensity, lSpeed * Time.deltaTime);
    }

    IEnumerator moveCycle()
    {
        //yield return new WaitForSeconds(0.25f);
        powerOn = true;
        yield return new WaitForSeconds(0.25f);
        powerOn = false;
        yield return new WaitForSeconds(2.0f);
        hasPoweredOn = true;
        redEmColor = Color.black;
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
