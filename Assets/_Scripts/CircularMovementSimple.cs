using UnityEngine;
using System.Collections;

public class CircularMovementSimple : MonoBehaviour
{

  public float forwardSpeed;
  public float turningSpeed;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    transform.Translate(0, 0, Time.deltaTime * forwardSpeed); // move forward
    transform.Rotate(0, Time.deltaTime * turningSpeed, 0); // turn a little
  }

}