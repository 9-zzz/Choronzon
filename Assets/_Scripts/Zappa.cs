using UnityEngine;
using System.Collections;

public class Zappa : MonoBehaviour
{

    public GameObject zappaMissile;
    public GameObject sp;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(ZappaFireCycle());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ZappaFireCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(60.0f);
            Instantiate(zappaMissile, sp.transform.position, sp.transform.rotation);
        }
    }

}
