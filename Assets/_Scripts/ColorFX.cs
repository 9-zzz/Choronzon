using UnityEngine;
using System.Collections;

public class ColorFX : MonoBehaviour
{

  public Color fColor;

  public float colorRange;
  public float changeRate;
  public float randRate;
  public float randRate2;
  public float randRate3;

  // Use this for initialization
  void Start()
  {
    this.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    randRate =  Random.Range(1.0f, 2.0f);
    randRate2 = Random.Range(1.0f, 2.0f);
    randRate3 = Random.Range(1.0f, 2.0f);
  }

  // Update is called once per frame
  void Update()
  {
    fColor = new Color
      (
  (Mathf.Abs(Mathf.Sin(Time.time * (changeRate*randRate)) * colorRange)),
  (Mathf.Abs(Mathf.Sin(Time.time * (changeRate*randRate2)) * colorRange)),
  (Mathf.Abs(Mathf.Sin(Time.time * (changeRate*randRate3)) * colorRange))
      );

    //this.GetComponent<Renderer>().material.SetColor("_EmissionColor", fColor);
    GetComponent<Renderer>().materials[3].SetColor("_EmissionColor", fColor);
  }

}
