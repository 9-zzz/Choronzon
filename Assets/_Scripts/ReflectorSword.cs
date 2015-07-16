using UnityEngine;
using System.Collections;

public class ReflectorSword : MonoBehaviour
{
    public Transform t;
    public static ReflectorSword S;
    public ParticleSystem swordClangFX;
    public ParticleSystem frippFX;
    public int powerCtr = 0;
    public Color swordOnColor;

    void Awake()
    {
        t = GetComponent<Transform>();
        S = this;
    }

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Renderer>().materials[1].EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FrippGem"))
        {
            var frippFXi = Instantiate(frippFX, other.transform.position, other.transform.rotation) as GameObject;
            Destroy(frippFXi, 1.0f);

            Destroy(other.gameObject);
            powerCtr = powerCtr + 1;
            if (powerCtr == 3)
            {
                transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", swordOnColor);
            }
        }

        if (other.tag == "faceBullet")
        {
            if (Melee.S.isSlashing)
            {
                other.GetComponent<FaceBullet>().wasReflected = true;
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
