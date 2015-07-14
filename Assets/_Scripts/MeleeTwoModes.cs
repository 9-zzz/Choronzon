using UnityEngine;
using System.Collections;

public class MeleeTwoModes : MonoBehaviour
{

    GameObject reflectorSword; // rs
    MeshRenderer rsmr;
    BoxCollider rsCollider;
    public Vector3 swordStartingpos;
    public Quaternion swordRot;

    public GameObject swordStatic;

    // Use this for initialization
    void Start()
    {
        reflectorSword = transform.GetChild(0).gameObject; // As log as sword is child on camera, is this a good idea? :(
        swordStartingpos = ReflectorSword.S.transform.localPosition;
        swordRot = ReflectorSword.S.transform.localRotation;
        //anim = GetComponent<Animation>();

        rsmr = reflectorSword.GetComponent<MeshRenderer>();
        rsCollider = reflectorSword.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rsmr.enabled = true;
            rsCollider.enabled = true;
        }
        else
        {
            rsmr.enabled = false;
            rsCollider.enabled = false;
        }

        if (Input.GetMouseButtonDown(2))
        {
            swapCamMode();
            if (Camera.main.GetComponent<TP_Camera>().enabled)
            {
                ReflectorSword.S.gameObject.transform.parent = transform.parent;
            }
            else
            {
                ReflectorSword.S.gameObject.transform.parent = Camera.main.transform;

                ReflectorSword.S.transform.localPosition = swordStartingpos;
                ReflectorSword.S.transform.localRotation = swordRot;
            }
        }
    }

    void swapCamMode()
    {
        Camera.main.GetComponent<TP_Camera>().enabled = !Camera.main.GetComponent<TP_Camera>().enabled;
        Camera.main.GetComponent<FP_Camera>().enabled = !Camera.main.GetComponent<FP_Camera>().enabled;
        //Camera.main.GetComponent<TP_Camera>().Distance = 0;
        Camera.main.transform.localPosition = new Vector3(0, 2, 0);
    }

    void SwordPlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //swordStatic.GetComponent<MeshRenderer>().enabled = false;
            //anim.Play();
        }
    }

}
