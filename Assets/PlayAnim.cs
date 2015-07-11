using UnityEngine;
using System.Collections;

public class PlayAnim : MonoBehaviour {

    Animation anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.Play("Something");
        }
	
	}
}
