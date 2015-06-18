using UnityEngine;
using System.Collections;

public class Melee : MonoBehaviour
{

    public GameObject swordStatic;
    Animation anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swordStatic.GetComponent<MeshRenderer>().enabled = false;
            anim.Play();
        }
        else if(!anim.isPlaying)
        {
            swordStatic.GetComponent<MeshRenderer>().enabled = true;
        }
    }

}