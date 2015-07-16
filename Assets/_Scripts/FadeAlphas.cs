using UnityEngine;
using System.Collections;

public class FadeAlphas : MonoBehaviour
{

    public Material trans;

    // Use this for initialization
    void Start()
    {
        trans.color = new Color(1, 0, 0, 1);

        transform.GetComponent<Renderer>().materials[0] = trans;

        //transform.GetComponent<Renderer>().material = trans;

        //for (int i = 0; i < transform.GetComponent<Renderer>().materials.Length; i++) transform.GetComponent<Renderer>().materials[i] = trans;


        // FUCK THIS METHOD
        //StartCoroutine(FadeTo(0.0f, 3.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x >= 0)
        transform.localScale -= new Vector3(0.006f, 0.006f, 0.006f);

        if (transform.localScale.x <= 0)
            Destroy(gameObject);
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        //float alpha = transform.GetComponent<Renderer>().material.color.a;
        float alpha = trans.color.a;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 0, 0, Mathf.Lerp(alpha, aValue, t));

            trans.color = newColor;

            yield return null;
        }

        Destroy(gameObject, 0.1f);
    }

}
