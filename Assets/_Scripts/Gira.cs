using UnityEngine;
using System.Collections;

public class Gira: MonoBehaviour
{

    public GameObject GiraContainer;
    public Rigidbody rbParent;
    public GameObject faceBullet;
    public GameObject shatteredPointyHands;
    public float smoothLookAtSpeed;
    public float distFromPlayer;

    public bool knockedOut = false;
    public bool isAttacking = false;

    public bool charging = false;
    public bool powerOn = false;
    public bool hasPoweredOn = false;
    public Color redEmColor;
    public Color originalRedEmColor;

    public float hp = 15;

    Light spLight;
    public GameObject sp; // Spawnpoint
    GameObject player;
    //Rigidbody rb;
    public Animator parentAnim;

    void Awake()
    {
        //originalRedEmColor = GetComponent<Renderer>().materials[0].color;
        //GetComponent<Renderer>().materials[0].SetColor("_EmissionColor", redEmColor);
        //player = GameObject.Find("CPlayer"); // *****************************************************!!!!@@@###$$$%%%
        player = Camera.main.gameObject.transform.GetChild(1).gameObject;
    }

    // Use this for initialization
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rbParent = transform.parent.GetComponent<Rigidbody>();
        parentAnim = transform.parent.GetComponent<Animator>();
        //sp = transform.GetChild(0).gameObject;
        spLight = sp.GetComponent<Light>();
        StartCoroutine(moveCycle());
        this.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().materials[0].SetColor("_EmissionColor", redEmColor);

        distFromPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distFromPlayer < 5)
        {
            isAttacking = true;
            parentAnim.SetBool("inAttackRange", true);
        }
        else
        {
            isAttacking = false;
            parentAnim.SetBool("inAttackRange", false);
        }


        if (!knockedOut) // So he stops turning and you can go attack his back.
        {
            //goSmoothLookAt(transform.parent.gameObject, player.transform.position);
            goSmoothLookAt(GiraContainer, player.transform.position);
            spSmoothLookAt(sp, player.transform.position);
            //spSmoothLookAt(sp.transform.parent.gameObject, player.transform.position);
            //SmoothLookAt(player.transform.position);
        }

        ChargingFX();

        if (hasPoweredOn == false) // Only once.
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
            // Wanted the emission to not be black, 
            // and have the glow on a Sin function 
            // TOO HARD RIGHT NOW, NOT IMPORTANT R/N
            //     var colorSlightEm = new Color(0.3f, 0.0f, 0.0f); // Slightly brighter neutral state, worked, but meh.
            ColorAndLightFX(Color.black, 5.0f, 0.0f, 5.0f); // Last color set to after power on.
        }
    }

    void ChargingFX()
    {
        if (charging)
        {
            ColorAndLightFX(Color.red, 2.0f, 0.45f, 2.0f);
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
        //redEmColor = Color.black; // Slight em color problem?
        while (true)
            if (!knockedOut )
            {
                {
                    rbParent.AddRelativeForce(0, 0, 2, ForceMode.VelocityChange);

                    if (Random.Range(0, 3) == 0)
                    {
                        rbParent.AddRelativeForce(1, 0, 3, ForceMode.VelocityChange);
                    }

                    if (distFromPlayer < 100)
                    {
                        rbParent.velocity = Vector3.zero;
                        rbParent.AddRelativeForce(0, 0, 5, ForceMode.VelocityChange);
                    }

                    if (distFromPlayer < 20)
                    {
                        yield return new WaitForSeconds(2.0f);
                        rbParent.velocity = Vector3.zero;
                        charging = true;
                        yield return new WaitForSeconds(0.4f);
                        FaceShoot();
                        yield return new WaitForSeconds(0.5f);
                        FaceShoot();
                        yield return new WaitForSeconds(0.5f);
                        FaceShoot();
                        yield return new WaitForSeconds(0.5f);
                        FaceShoot();
                        yield return new WaitForSeconds(0.5f);
                        FaceShoot();
                        charging = false;
                    }
                    yield return new WaitForSeconds(2.5f);
                }
            }
            else
            {
                // If Gira is knocked out
                yield return new WaitForSeconds(10.0f);
                knockedOut = false;
                parentAnim.SetBool("knockedOut", false);
                hp = 15;
            }
    } // End of Move Cycle Coroutine.

    void FaceShoot()
    {
        Instantiate(faceBullet, sp.transform.position, sp.transform.rotation);
    }

    void spSmoothLookAt(GameObject go, Vector3 pointToLookAt)
    {
        Vector3 newDirection = pointToLookAt - go.transform.position; // Get the direction to look in.
        go.transform.rotation = Quaternion.Slerp(go.transform.rotation, Quaternion.LookRotation(newDirection), (smoothLookAtSpeed * 4.0f) * Time.deltaTime);
    }

    void goSmoothLookAt(GameObject go, Vector3 pointToLookAt)
    {
        Vector3 newDirection = pointToLookAt - go.transform.position; // Get the direction to look in.
        newDirection.y = 0;
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

    IEnumerator hurtFlash(int flashes)
    {
        rbParent.velocity = Vector3.zero; // Stop when hurt?
        var flashTime = 0.04f;
        for (int i = 0; i < flashes; i++)
        {
            redEmColor = Color.red;
            yield return new WaitForSeconds(flashTime);
            redEmColor = Color.black;
            yield return new WaitForSeconds(flashTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "sword")
        {
            if (knockedOut)
            {
                // Issues with spawning at centers of mass on rigged armature...
                Instantiate(shatteredPointyHands, GiraContainer.transform.position + GiraContainer.transform.up * 2.56f, GiraContainer.transform.rotation);
                Destroy(gameObject);
            }
        }

        if (other.tag == "faceBullet")
        {
            // So stray bullets can't hit more than one Gira, Maybe that's mean....
            //if (other.GetComponent<Collider>() != null) other.GetComponent<Collider>().enabled = false;

            hp -= 5.0f;
            if (hp == 0)
            {
                knockedOut = true;
                parentAnim.SetBool("knockedOut", true);
                StartCoroutine(hurtFlash(70));
            }
            StartCoroutine(hurtFlash(16));
        }
    }

}
