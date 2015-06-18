using UnityEngine;
using System.Collections;

public class HeadTurrentFireCycle : MonoBehaviour
{

    public float waitTime;
    public float rotateSpeed;
    public bool isFiring = false;
    public GameObject missile;

    Transform sp;
    ParticleSystem ps;

    void Awake()
    {
        sp = transform.GetChild(0);
        ps = sp.transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(fireAndWait());
        //fireAndWait();
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.IsAlive())
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }

    IEnumerator fireAndWait()
    {
        while (isFiring)
        {
            yield return new WaitForSeconds(waitTime);

            if (!ps.IsAlive())
            {
                Instantiate(missile, sp.position, sp.rotation);
                ps.Play();
            }
        }
    }

}