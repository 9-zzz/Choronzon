using UnityEngine;
using System.Collections;

public class AutoRotator : MonoBehaviour
{
    public float xRot;
    public float yRot;
    public float zRot;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xRot * Time.deltaTime, yRot * Time.deltaTime, zRot * Time.deltaTime);
    }

}
