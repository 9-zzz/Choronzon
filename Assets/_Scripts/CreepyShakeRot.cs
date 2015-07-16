using UnityEngine;
using System.Collections;

public class CreepyShakeRot : MonoBehaviour
{

  public float[] rots;
  public float[] creepTimes;
  public float creepTime;
  public float rotVal;


  public bool hit = false;

  GameObject ArcherRef;

  // Use this for initialization
  void Start()
  {
    rots = new float[6];

    rots[0] = -rotVal;
    rots[1] = rotVal;
    rots[2] = -rotVal;
    rots[3] = rotVal;
    rots[4] = -rotVal;
    rots[5] =  rotVal;

    creepTimes = new float[3];

    creepTimes[0] = 20;
    creepTimes[1] = 30;
    creepTimes[2] = 60;

    ArcherRef = GameObject.Find("CPlayer");
    StartCoroutine(creepyTwitchRotate());
  }

  // Update is called once per frame
  void Update()
  {

  //rotVal = Mathf.Sin(Time.time * 10) * 10;

    if (hit)
      transform.localScale -= new Vector3(0.7f, 0.7f, 0.7f);

    if (transform.localScale.x <= 0)
    {
      //Instantiate(orbPUPFX, transform.position, transform.rotation);
      Destroy(gameObject);
    }

    //if (Vector3.Distance(ArcherRef.transform.position, transform.position) < 40)
      transform.LookAt(ArcherRef.transform.position);

  }

  IEnumerator creepyTwitchRotate()
  {
    creepTime = creepTimes[Random.Range(0, 3)];
    var i = 0;
    for (i = 0; i < creepTime; i++)
    {
      transform.Rotate(rots[Random.Range(0, 2)], 0, 0);
      transform.Rotate(0, rots[Random.Range(2, 4)], 0);
      transform.Rotate(0, 0, rots[Random.Range(4, 6)]);
      yield return new WaitForSeconds(0.01f);
      this.GetComponent<ColorFX>().enabled = false;

      if (i == (creepTime - 1))
      {
        yield return new WaitForSeconds(1f);
      this.GetComponent<ColorFX>().enabled = true;
        creepTime = creepTimes[Random.Range(0, 3)];
        i = 0;
      }
    }
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "arrow")
    {
    }
  }

}
