using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageHandler : MonoBehaviour
{


    public float damageFlashTime;
    public float missileDamageAmt;
    Image fader;

    void Awake()
    {
        fader = GameObject.Find("Fader").GetComponent<Image>();
    }

    // Use this for initialization
    void Start()
    {
        fader.CrossFadeAlpha(1, 1, true);
        fader.CrossFadeAlpha(0, 6, true);
        Color tmp = new Color(253.0f / 255.0f, 20.0f / 255.0f, 73.0f / 255.0f, 180.0f / 255.0f);
        fader.color = tmp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "missile")
        {
            StartCoroutine(damageFlash());
            HealthBar.S.hp -= missileDamageAmt;
        }
    }

    IEnumerator damageFlash()
    {
        fader.CrossFadeAlpha(1, damageFlashTime, true);
        yield return new WaitForSeconds(damageFlashTime);
        fader.CrossFadeAlpha(0, damageFlashTime, true);
    }

}