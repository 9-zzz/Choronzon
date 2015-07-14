using UnityEngine;
using System.Collections;

public class Melee : MonoBehaviour
{
    public static Melee S;

    public Animator anim;
    //public Animation animation;
    GameObject reflectorSword; // rs
    public BoxCollider rsCollider;
    public MeshRenderer rsmr;

    public Vector3 swordStartingpos;
    public Quaternion swordRot;
    public GameObject swordStatic;

    public bool isSlashing = false;

    // On Main Camera
    void Awake()
    {
        S = this;
        reflectorSword = transform.GetChild(0).gameObject; // As log as sword is child on camera, is this a good idea? :(
        anim = reflectorSword.GetComponent<Animator>();
        //animation = reflectorSword.GetComponent<Animation>();
    }

    // Use this for initialization
    void Start()
    {
        reflectorSword = transform.GetChild(0).gameObject; // As log as sword is child on camera, is this a good idea? :(
        swordStartingpos = ReflectorSword.S.transform.localPosition;
        swordRot = ReflectorSword.S.transform.localRotation;
        //anim = GetComponent<Animation>();

        rsmr = reflectorSword.GetComponent<MeshRenderer>();
        rsCollider = reflectorSword.GetComponent<BoxCollider>();
        //rsCollider.enabled = false;
    }

    void ToggleSword()
    {
        rsmr.enabled = !rsmr.enabled;
        rsCollider.enabled = !rsCollider.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSlashing)
        {
            isSlashing = true; // So he doesn't just hold the sword to reflect bullets.
            StartCoroutine(SlashTimeBool());
            anim.SetInteger("swordState", 1);
        }

        //if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("swordTestCube")) { isSlashing = true; } else { print("not slashing"); }

        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("swordTestCube")) anim.SetInteger("swordState", 0);
    }

     public IEnumerator SlashTimeBool()
     {
         yield return new WaitForSeconds(1.0f);
         isSlashing = false;
         anim.SetInteger("swordState", 0);
     }

}
