using UnityEngine;
using System.Collections;

public class BirdBrain : MonoBehaviour
{

  public float hlRate;
  public float hlRange;
  public float xRot;
  public float yRot;
  public float zRot;

  public Color oscilColor;

  Light heartLight;

  // Use this for initialization
  void Start()
  {
    heartLight = transform.GetChild(0).GetComponent<Light>();
    this.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
  }

  // Update is called once per frame
  void Update()
  {

  heartLight.intensity = Mathf.Sin(Time.time * hlRate) * hlRange;
  heartLight.range = Mathf.Abs(Mathf.Sin(Time.time * hlRate/7) * hlRange*2);

  oscilColor = new Color((Mathf.Abs(Mathf.Sin(Time.time * (hlRate / 7.0f)) * 1.0f)), 0.0f, 0.0f);

  this.GetComponent<Renderer>().material.SetColor("_EmissionColor", oscilColor);

  transform.Rotate(Time.deltaTime*xRot, Time.deltaTime*yRot, Time.deltaTime*zRot);

  }

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Terrain")
    {
      transform.GetComponent<Rigidbody>().AddForce(0, 10, 0, ForceMode.Impulse);
      print("!$%$@#%#$%#$%#@$%");
    }

    if (other.tag == "arrow")
    {
      other.transform.parent = transform;
      if(transform.parent != null)
      {
      this.GetComponent<Rigidbody>().useGravity = true;
      //transform.parent.GetComponent<CircularMovementSimple>().turningSpeed = 999;
      //transform.parent.GetComponent<CircularMovementSimple>().forwardSpeed *= 2;
      }
      transform.parent = null;
    }

  }

}