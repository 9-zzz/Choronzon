using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{

    public float speed;
    public bool lokat = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (lokat)
            SmoothLookAt(GameObject.FindGameObjectWithTag("Player").transform.position);

    }

    void SmoothLookAt(Vector3 newDirection)
    {
        newDirection -= this.transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDirection), speed * Time.deltaTime);
    }

}